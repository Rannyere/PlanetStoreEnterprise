using PSE.Core.Responses;
using PSE.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Services.Interfaces;

public interface ICustomerService
{
    Task<AddressViewModel> GetAddress();

    Task<ResponseErrorResult> AddAddress(AddressViewModel address);
}