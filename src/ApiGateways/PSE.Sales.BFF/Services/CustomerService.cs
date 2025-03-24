using Microsoft.Extensions.Options;
using PSE.Sales.BFF.DTOs;
using PSE.Sales.BFF.Extensions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PSE.Sales.BFF.Services;

public interface ICustomerService
{
    Task<AddressDTO> GetAddress();
}

public class CustomerService : Service, ICustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.CustomerUrl);
    }

    public async Task<AddressDTO> GetAddress()
    {
        var response = await _httpClient.GetAsync("/customer/address/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        CheckErrorsResponse(response);

        return await DeserializeObjectResponse<AddressDTO>(response);
    }
}