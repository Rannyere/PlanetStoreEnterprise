using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly HttpClient _httpClient;

        public AuthenticateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginTokenResponse> LoginUser(LoginUser loginUser)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(loginUser),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:20840/api/account/login", loginContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<UserLoginTokenResponse>(await response.Content.ReadAsStringAsync(), options);          
        }

        public async Task<UserLoginTokenResponse> RegisterUser(RegisterUser registerUser)
        {
            var registerContent = new StringContent(
                JsonSerializer.Serialize(registerUser),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:20840/api/account/register", registerContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<UserLoginTokenResponse>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}
