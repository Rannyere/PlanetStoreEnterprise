using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.Sales.BFF.DTOs;
using PSE.Sales.BFF.Extensions;

namespace PSE.Sales.BFF.Services
{
    public interface ICatalogService
    {
        Task<ProductCatalogDTO> GetProductById(Guid id);
    }

    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
        }

        public async Task<ProductCatalogDTO> GetProductById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalog/products/{id}");

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<ProductCatalogDTO>(response);
        }
    }
}
