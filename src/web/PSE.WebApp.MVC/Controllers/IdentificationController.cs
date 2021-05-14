using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services.Interfaces;

namespace PSE.WebApp.MVC.Controllers
{
    public class IdentificationController : MainController
    {
        private readonly IAuthenticateService _authenticateService;

        public IdentificationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpGet]
        [Route("new-account")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("new-account")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return View(registerUser);

            var response = await _authenticateService.RegisterUser(registerUser);

            if (HasErrorsInResponse(response.ResponseErrorResult)) return View(registerUser);

            await _authenticateService.ConnectAccount(response);

            return RedirectToAction("Index", "Catalog");

        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginUser loginUser, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(loginUser);

            var response = await _authenticateService.LoginUser(loginUser);

            if (HasErrorsInResponse(response.ResponseErrorResult)) return View(loginUser);

            await _authenticateService.ConnectAccount(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Catalog");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("exit")]
        public async Task<IActionResult> Logout()
        {
            await _authenticateService.Logout();
            return RedirectToAction("Index", "Catalog");
        } 
    }
}
