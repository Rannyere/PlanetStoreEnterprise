using System;
using System.Net.Http;
using PSE.WebApp.MVC.Extensions;

namespace PSE.WebApp.MVC.Services
{
    public abstract class Service
    {
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
    }
}
