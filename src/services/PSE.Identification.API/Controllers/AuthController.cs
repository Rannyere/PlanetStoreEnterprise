using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PSE.Core.Messages.Integration;
using PSE.Identification.API.Models;
using PSE.Identification.API.Services;
using PSE.MessageBus;
using PSE.WebAPI.Core.Controllers;

namespace PSE.Identification.API.Controllers
{
    [Route("api/account")]
    public class AuthController : MainController
    {
        private readonly AuthenticationService _authenticationService;
        private IMessageBus _bus;

        public AuthController(AuthenticationService authenticationService,
                              IMessageBus bus)
        {
            _authenticationService = authenticationService;
            _bus = bus;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _authenticationService._userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                // integration create customer PSE.Clients.API
                var customerResult = await RegisterCustomer(registerUser);

                if(!customerResult.ValidationResult.IsValid)
                {
                    await _authenticationService._userManager.DeleteAsync(user);
                    return CustomResponse(customerResult.ValidationResult);
                }

                return CustomResponse(await _authenticationService.GenerateJwt(registerUser.Email));
            }

            foreach (var error in result.Errors)
            {
                AddErrorInProcess(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authenticationService._signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authenticationService.GenerateJwt(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                AddErrorInProcess("User temporarily blocked by invalid attempts");
                return CustomResponse();
            }

            AddErrorInProcess("Incorrect username or password");
            return CustomResponse();
        }

        private async Task<ResponseMessage> RegisterCustomer(RegisterUser registerUser)
        {
            var user = await _authenticationService._userManager.FindByEmailAsync(registerUser.Email);
            var userRegistered = new UserRegisteredIntegrationEvent(
                Guid.Parse(user.Id), registerUser.Name, registerUser.Email, registerUser.Cpf);

            try
            {
                return await _bus.RequestAsync<UserRegisteredIntegrationEvent, ResponseMessage>(userRegistered);
            }
            catch
            {
                await _authenticationService._userManager.DeleteAsync(user);
                throw;
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                AddErrorInProcess("Invalid RefreshToken");
                return CustomResponse();
            }

            var token = await _authenticationService.GetRefreshToken(Guid.Parse(refreshToken));

            if (token is null)
            {
                AddErrorInProcess("Expired RefreshToken");
                return CustomResponse();
            }

            return CustomResponse(await _authenticationService.GenerateJwt(token.Username));
        }
    }
}
