using System;
using System.Collections.Generic;
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
        Task<IEnumerable<ProductCatalogDTO>> GetItems(IEnumerable<Guid> ids);
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

        public async Task<IEnumerable<ProductCatalogDTO>> GetItems(IEnumerable<Guid> ids)
        {
            var idsRequest = string.Join(",", ids);

            var response = await _httpClient.GetAsync($"/catalog/products/list/{idsRequest}/");

            CheckErrorsResponse(response);

            return await DeserializeObjectResponse<IEnumerable<ProductCatalogDTO>>(response);
        }
    }
}
