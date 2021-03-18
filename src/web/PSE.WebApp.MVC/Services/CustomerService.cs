using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.Core.Responses;
using PSE.WebApp.MVC.Extensions;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services.Interfaces;

namespace PSE.WebApp.MVC.Services
{
    public class CustomerService : Service, ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri(settings.Value.CustomerUrl);
        }

        public async Task<AddressViewModel> GetAddress()
        {
            var response = await _httpClient.GetAsync("/customer/address/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<AddressViewModel>(response);
        }

        public async Task<ResponseErrorResult> AddAddress(AddressViewModel address)
        {
            var addressContent = GetContent(address);

            var response = await _httpClient.PostAsync("/customer/address/", addressContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        
    }
}
