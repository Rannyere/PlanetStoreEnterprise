using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.WebApp.MVC.Extensions;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services.Interfaces;

namespace PSE.WebApp.MVC.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
            _httpClient = httpClient;
        }

        public async Task<PagedViewModel<ProductViewModel>> GetAllProducts(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalog/products?ps={pageSize}&page={pageIndex}&q={query}");

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<PagedViewModel<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetProductById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalog/products/{id}");

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<ProductViewModel>(response);
        }
    }
}
