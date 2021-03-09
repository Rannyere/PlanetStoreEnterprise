using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services.Interfaces;

namespace PSE.WebApp.MVC.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ISalesBffService _salesBffService;
       
        public CartController(ISalesBffService salesBffService)                   
        {
            _salesBffService = salesBffService;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _salesBffService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-product")]
        public async Task<IActionResult> AddProductCart(ItemCartViewModel itemCart)
        {
            var response = await _salesBffService.AddProductCart(itemCart);

            if (HasErrorsInResponse(response)) return View("Index", await _salesBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-product")]
        public async Task<IActionResult> UpdateProductCart(Guid productId, int quantity)
        {
            var item = new ItemCartViewModel { ProductId = productId, Quantity = quantity };
            var response = await _salesBffService.UpdateProductCart(productId, item);

            if (HasErrorsInResponse(response)) return View("Index", await _salesBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/remove-product")]
        public async Task<IActionResult> RemoveProductCart(Guid productId)
        {
            var response = await _salesBffService.RemoveProductCart(productId);

            if (HasErrorsInResponse(response)) return View("Index", await _salesBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var response = await _salesBffService.ApplyVoucherCart(voucherCode);

            if (HasErrorsInResponse(response)) return View("Index", await _salesBffService.GetCart());

            return RedirectToAction("Index");
        }
    }
}