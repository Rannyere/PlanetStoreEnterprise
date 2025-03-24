using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using PSE.Core.Responses;
using PSE.WebAPI.Core.User;
using PSE.WebApp.MVC.Extensions;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Services;

public class AuthenticateService : Service, IAuthenticateService
{
    private readonly HttpClient _httpClient;

    private readonly IAspNetUser _aspNetUser;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticateService(HttpClient httpClient,
                               IOptions<AppSettings> settings,
                               IAspNetUser aspNetUser,
                               IAuthenticationService authenticationService)
    {
        httpClient.BaseAddress = new Uri(settings.Value.IdentificationUrl);
        _httpClient = httpClient;
        _aspNetUser = aspNetUser;
        _authenticationService = authenticationService;
    }

    public async Task<UserLoginTokenResponse> LoginUser(LoginUser loginUser)
    {
        var loginContent = GetContent(loginUser);

        var response = await _httpClient.PostAsync("/api/account/login", loginContent);

        if (!CheckErrorsResponse(response))
        {
            return new UserLoginTokenResponse
            {
                ResponseErrorResult = await DeserializeObjectResponse<ResponseErrorResult>(response)
            };
        }

        return await DeserializeObjectResponse<UserLoginTokenResponse>(response);
    }

    public async Task<UserLoginTokenResponse> RegisterUser(RegisterUser registerUser)
    {
        var registerContent = GetContent(registerUser);

        var response = await _httpClient.PostAsync("api/account/register", registerContent);

        if (!CheckErrorsResponse(response))
        {
            return new UserLoginTokenResponse
            {
                ResponseErrorResult = await DeserializeObjectResponse<ResponseErrorResult>(response)
            };
        }

        return await DeserializeObjectResponse<UserLoginTokenResponse>(response);
    }

    public async Task<UserLoginTokenResponse> UseRefreshToken(string refreshToken)
    {
        var refreshTokenContent = GetContent(refreshToken);

        var response = await _httpClient.PostAsync("/api/account/refresh-token", refreshTokenContent);

        if (!CheckErrorsResponse(response))
        {
            return new UserLoginTokenResponse
            {
                ResponseErrorResult = await DeserializeObjectResponse<ResponseErrorResult>(response)
            };
        }

        return await DeserializeObjectResponse<UserLoginTokenResponse>(response);
    }

    public async Task ConnectAccount(UserLoginTokenResponse userLoginTokenResponse)
    {
        var token = GetFormatedToken(userLoginTokenResponse.AccessToken);

        var claims = new List<Claim>();
        claims.Add(new Claim("JWT", userLoginTokenResponse.AccessToken));
        claims.Add(new Claim("RefreshToken", userLoginTokenResponse.RefreshToken));
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
            IsPersistent = true
        };

        await _authenticationService.SignInAsync(
            _aspNetUser.GetHttpContext(),
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    public async Task Logout()
    {
        await _authenticationService.SignOutAsync(
            _aspNetUser.GetHttpContext(),
            CookieAuthenticationDefaults.AuthenticationScheme, null);
    }

    public static JwtSecurityToken GetFormatedToken(string jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }

    public bool TokenExpired()
    {
        var jwt = _aspNetUser.GetUserToken();

        if (jwt is null) return false;

        var token = GetFormatedToken(jwt);
        return token.ValidTo.ToLocalTime() < DateTime.Now;
    }

    public async Task<bool> RefreshTokenIsValid()
    {
        var response = await UseRefreshToken(_aspNetUser.GetUserRefreshToken());

        if (response.AccessToken != null && response.ResponseErrorResult == null)
        {
            await ConnectAccount(response);
            return true;
        }

        return false;
    }
}