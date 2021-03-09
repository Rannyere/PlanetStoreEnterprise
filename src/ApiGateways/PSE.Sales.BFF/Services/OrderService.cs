using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.Sales.BFF.DTOs;
using PSE.Sales.BFF.Extensions;

namespace PSE.Sales.BFF.Services
{
    public interface IOrderService
    {
        Task<VoucherDTO> GetVoucherByCode(string code);
    }

    public class OrderService : Service, IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.OrderUrl);
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var response = await _httpClient.GetAsync($"/voucher/{code}/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<VoucherDTO>(response);
        }
    }
}
