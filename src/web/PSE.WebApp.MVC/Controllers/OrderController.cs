using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services.Interfaces;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Controllers;

[Authorize]
public class OrderController : MainController
{
    private readonly ICustomerService _customerService;
    private readonly ISalesBffService _salesBffService;

    public OrderController(ICustomerService customerService,
                           ISalesBffService salesBffService)
    {
        _customerService = customerService;
        _salesBffService = salesBffService;
    }

    [HttpGet]
    [Route("delivery-address")]
    public async Task<IActionResult> AddressDelivery()
    {
        var cart = await _salesBffService.GetCart();
        if (cart.Items.Count == 0) return RedirectToAction("Index", "Cart");

        var address = await _customerService.GetAddress();
        var order = _salesBffService.MappingToOrder(cart, address);

        return View(order);
    }

    [HttpGet]
    [Route("payment")]
    public async Task<IActionResult> Payment()
    {
        var cart = await _salesBffService.GetCart();
        if (cart.Items.Count == 0) return RedirectToAction("Index", "Cart");

        var order = _salesBffService.MappingToOrder(cart, null);

        return View(order);
    }

    [HttpPost]
    [Route("checkout")]
    public async Task<IActionResult> Checkout(OrderTransactionViewModel orderTransaction)
    {
        if (!ModelState.IsValid) return View("Payment", _salesBffService.MappingToOrder(
            await _salesBffService.GetCart(), null));

        var response = await _salesBffService.Checkout(orderTransaction);

        if (HasErrorsInResponse(response))
        {
            var cart = await _salesBffService.GetCart();
            if (cart.Items.Count == 0) return RedirectToAction("Index", "Cart");

            var orderMap = _salesBffService.MappingToOrder(cart, null);
            return View("Payment", orderMap);
        }

        return RedirectToAction("OrderCompleted");
    }

    [HttpGet]
    [Route("order-completed")]
    public async Task<IActionResult> OrderCompleted()
    {
        return View("ConfirmationOrder", await _salesBffService.GetLastOrder());
    }

    [HttpGet("my-orders")]
    public async Task<IActionResult> MyOrders()
    {
        return View(await _salesBffService.GetListOrdersByCustomerId());
    }
}