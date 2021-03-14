using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Services.Interfaces;

namespace PSE.WebApp.MVC.Controllers
{
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
        public async Task<IActionResult> EnderecoEntrega()
        {
            var cart = await _salesBffService.GetCart();
            if (cart.Items.Count == 0) return RedirectToAction("Index", "Cart");

            var address = await _customerService.GetAddress();
            var order = _salesBffService.MappingToOrder(cart, address);

            return View(order);
        }
    }
}
