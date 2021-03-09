using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.Core.Responses;
using PSE.WebApp.MVC.Extensions;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services.Interfaces;

namespace PSE.WebApp.MVC.Services
{
    public class AuthenticateService : Service, IAuthenticateService
    {
        private readonly HttpClient _httpClient;

        public AuthenticateService(HttpClient httpClient,
                                   IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.IdentificationUrl);

            _httpClient = httpClient;
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
    }
}
