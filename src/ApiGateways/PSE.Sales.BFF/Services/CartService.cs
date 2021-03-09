using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.Core.Responses;
using PSE.Sales.BFF.DTOs;
using PSE.Sales.BFF.Extensions;

namespace PSE.Sales.BFF.Services
{
    public interface ICartService
    {
        Task<CartCustomerDTO> GetCart();
        Task<ResponseErrorResult> AddProductCart(ItemCartDTO product);
        Task<ResponseErrorResult> UpdateProductCart(Guid productId, ItemCartDTO product);
        Task<ResponseErrorResult> RemoveProductCart(Guid productId);
        Task<ResponseErrorResult> ApplyVoucherCart(VoucherDTO voucher);
    }

    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<CartCustomerDTO> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart/");

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<CartCustomerDTO>(response);
        }

        public async Task<ResponseErrorResult> AddProductCart(ItemCartDTO item)
        {
            var itemContent = GetContent(item);

            var response = await _httpClient.PostAsync("/cart", itemContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseErrorResult> UpdateProductCart(Guid produtoId, ItemCartDTO item)
        {
            var itemContent = GetContent(item);

            var response = await _httpClient.PutAsync($"/cart/{item.ProductId}", itemContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseErrorResult> RemoveProductCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{productId}");

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseErrorResult> ApplyVoucherCart(VoucherDTO voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync("/cart/apply-voucher/", itemContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }
    }
}
