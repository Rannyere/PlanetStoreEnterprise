using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.WebApp.MVC.Extensions;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<CartCustomerViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart/");

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<CartCustomerViewModel>(response);
        }

        public async Task<ResponseErrorResult> AddProductCart(CartProductViewModel product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PostAsync("/cart", itemContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseErrorResult> UpdateProductCart(Guid produtoId, CartProductViewModel product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PutAsync($"/cart/{product.ProductId}", itemContent);

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseErrorResult> RemoveProductCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{productId}");

            if (!CheckErrorsResponse(response)) return await DeserializeObjectResponse<ResponseErrorResult>(response);

            return ReturnOk();
        }
    }
}