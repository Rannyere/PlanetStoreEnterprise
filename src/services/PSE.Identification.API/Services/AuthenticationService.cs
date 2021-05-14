using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.JwtSigningCredentials.Interfaces;
using PSE.Identification.API.Data;
using PSE.Identification.API.Extensions;
using PSE.Identification.API.Models;
using PSE.WebAPI.Core.Identification;
using PSE.WebAPI.Core.User;

namespace PSE.Identification.API.Services
{
    public class AuthenticationService
    {
        public readonly SignInManager<IdentityUser> _signInManager;
        public readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly AppTokenSettings _appTokenSettings;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IAspNetUser _aspNetUser;
        private readonly IJsonWebKeySetService _jwksService;

        public AuthenticationService(SignInManager<IdentityUser> signInManager,
                                     UserManager<IdentityUser> userManager,
                                     IOptions<AppSettings> appSettings,
                                     IOptions<AppTokenSettings> appTokenSettings,
                                     ApplicationDbContext applicationDbContext,
                                     IAspNetUser aspNetUser,
                                     IJsonWebKeySetService jwksService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _appTokenSettings = appTokenSettings.Value;
            _applicationDbContext = applicationDbContext;
            _aspNetUser = aspNetUser;
            _jwksService = jwksService;
        }

        public async Task<UserLoginTokenResponse> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var claimsIdentity = await GetClaimsUser(claims, user);
            var encodedToken = CodifyToken(claimsIdentity);

            var refreshToken = await GenerateRefreshToken(email);

            return GetResponseToken(encodedToken, user, claims, refreshToken);
        }

        private async Task<ClaimsIdentity> GetClaimsUser(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //Jti == id token
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString())); //date token expiration
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)); //date token issuer

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);

            return claimsIdentity;
        }

        private string CodifyToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var currentIssuer = $"{_aspNetUser.GetHttpContext().Request.Scheme}://{_aspNetUser.GetHttpContext().Request.Host}";

            var key = _jwksService.GetCurrent();

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = currentIssuer,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = key
            });

            return tokenHandler.WriteToken(token);
        }

        private UserLoginTokenResponse GetResponseToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims, RefreshToken refreshToken)
        {
            return new UserLoginTokenResponse
            {
                AccessToken = encodedToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private async Task<RefreshToken> GenerateRefreshToken(string email)
        {
            var refreshToken = new RefreshToken
            {
                Username = email,
                ExpirationDate = DateTime.UtcNow.AddHours(_appTokenSettings.RefreshTokenExpiration)
            };

            _applicationDbContext.RefreshTokens.RemoveRange(_applicationDbContext.RefreshTokens.Where(u => u.Username == email));
            await _applicationDbContext.RefreshTokens.AddAsync(refreshToken);

            await _applicationDbContext.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<RefreshToken> GetRefreshToken(Guid refreshToken)
        {
            var token = await _applicationDbContext.RefreshTokens.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Token == refreshToken);

            return token != null && token.ExpirationDate.ToLocalTime() > DateTime.Now
                ? token
                : null;
        }
    }
}
