using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.Core.Responses;
using PSE.WebApp.MVC.Extensions;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services.Interfaces;

namespace PSE.WebApp.MVC.Services
{
    public class SalesBffService : Service, ISalesBffService
    {
        private readonly HttpClient _httpClient;

        public SalesBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.SalesUrl);
        }

        public async Task<CartCustomerViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/sales/cart");

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<CartCustomerViewModel>(response);
        }


        public async Task<int> GetQuantityProductsInCart()
        {
            var response = await _httpClient.GetAsync("/sales/cart-quantity");

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<int>(response);
        }

        public async Task<ResponseErrorResult> AddProductCart(ItemCartViewModel itemCart)
        {
            var itemContent = GetContent(itemCart);

            var response = await _httpClient.PostAsync("/sales/cart/product", itemContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseErrorResult> UpdateProductCart(Guid productId, ItemCartViewModel itemCart)
        {
            var itemContent = GetContent(itemCart);

            var response = await _httpClient.PutAsync($"/sales/cart/product/{itemCart.ProductId}", itemContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseErrorResult> RemoveProductCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/sales/cart/product/{productId}");

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseErrorResult> ApplyVoucherCart(string voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync($"/sales/cart/apply-voucher/", itemContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }
    }
}