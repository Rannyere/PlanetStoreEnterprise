using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSE.Clients.API.Application.Commands;
using PSE.Clients.API.Models;
using PSE.Core.Mediator;
using PSE.WebAPI.Core.Controllers;
using PSE.WebAPI.Core.User;
using System.Threading.Tasks;

namespace PSE.Clients.API.Controllers;

[Authorize]
public class CustomerController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IAspNetUser _user;
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(IMediatorHandler mediatorHandler,
                              IAspNetUser user,
                              ICustomerRepository customerRepository)
    {
        _mediatorHandler = mediatorHandler;
        _user = user;
        _customerRepository = customerRepository;
    }

    [HttpGet("customer/address")]
    public async Task<IActionResult> GetAddress()
    {
        var address = await _customerRepository.GetAddressById(_user.GetUserId());

        return address == null ? NotFound() : CustomResponse(address);
    }

    [HttpPost("customer/address")]
    public async Task<IActionResult> AddAddress(AddAddressCommand address)
    {
        address.CustomerId = _user.GetUserId();

        return CustomResponse(await _mediatorHandler.SendCommand(address));
    }
}