using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Controllers;

public class CustomerController : MainController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<ActionResult> NewAddress(AddressViewModel address)
    {
        var response = await _customerService.AddAddress(address);

        if (HasErrorsInResponse(response)) TempData["Errors"] =
            ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

        return RedirectToAction("AddressDelivery", "Order");
    }
}