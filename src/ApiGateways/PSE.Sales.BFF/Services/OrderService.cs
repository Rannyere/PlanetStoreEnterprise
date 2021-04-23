using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.Core.Responses;
using PSE.Sales.BFF.DTOs;
using PSE.Sales.BFF.Extensions;

namespace PSE.Sales.BFF.Services
{
    public interface IOrderService
    {
        Task<ResponseErrorResult> Checkout(OrderDTO order);
        Task<OrderDTO> GetLastOrder();
        Task<IEnumerable<OrderDTO>> GetListOrdersByCustomerId();
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

        public async Task<ResponseErrorResult> Checkout(OrderDTO order)
        {
            var orderContent = GetContent(order);

            var response = await _httpClient.PostAsync("/order/", orderContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        public async Task<OrderDTO> GetLastOrder()
        {
            var response = await _httpClient.GetAsync("/order/last/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<OrderDTO>(response);
        }

        public async Task<IEnumerable<OrderDTO>> GetListOrdersByCustomerId()
        {
            var response = await _httpClient.GetAsync("/order/list/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<IEnumerable<OrderDTO>>(response);
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
