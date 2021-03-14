using System;
using System.Threading.Tasks;
using PSE.Core.Responses;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<AddressViewModel> GetAddress();

        Task<ResponseErrorResult> AddAddress(AddressViewModel address);
    }
}
