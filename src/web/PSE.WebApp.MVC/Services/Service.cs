using PSE.Core.Responses;
using PSE.WebApp.MVC.Extensions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Services;

public abstract class Service
{
    protected StringContent GetContent(object dado)
    {
        return new StringContent(
            JsonSerializer.Serialize(dado),
            Encoding.UTF8,
            "application/json");
    }

    protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
    }

    protected bool CheckErrorsResponse(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpResponseException(response.StatusCode);

            //in this case, there is an error message within the API response
            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }

    protected ResponseErrorResult ReturnOk()
    {
        return new ResponseErrorResult();
    }
}