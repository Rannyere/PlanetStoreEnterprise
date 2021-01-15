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

        public async Task<string> LoginUser(LoginUser loginUser)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(loginUser),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:20840/api/account/login", loginContent);

            var test = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> RegisterUser(RegisterUser registerUser)
        {
            var registerContent = new StringContent(
                JsonSerializer.Serialize(registerUser),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:20840/api/account/register", registerContent);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
