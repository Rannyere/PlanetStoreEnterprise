using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services;

namespace PSE.WebApp.MVC.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;

        public CartController(ICartService cartService,
                              ICatalogService catalogService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-product")]
        public async Task<IActionResult> AddProductCart(CartProductViewModel productModel)
        {
            var productBase = await _catalogService.GetProductById(productModel.ProductId);

            ValidateProductCart(productBase, productModel.Quantity);
            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            productModel.Name = productBase.Name;
            productModel.Value = productBase.Value;
            productModel.Image = productBase.Image;

            var response = await _cartService.AddProductCart(productModel);

            if (HasErrorsInResponse(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-product")]
        public async Task<IActionResult> UpdateProductCart(Guid productId, int quantity)
        {
            var productBase = await _catalogService.GetProductById(productId);

            ValidateProductCart(productBase, quantity);
            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            var productModel = new CartProductViewModel { ProductId = productId, Quantity = quantity };
            var response = await _cartService.UpdateProductCart(productId, productModel);

            if (HasErrorsInResponse(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/remove-product")]
        public async Task<IActionResult> RemoveProductCart(Guid productId)
        {
            var productBase = await _catalogService.GetProductById(productId);

            if (productBase == null)
            {
                AddErrorValidation("Product not found!");
                return View("Index", await _cartService.GetCart());
            }

            var response = await _cartService.RemoveProductCart(productId);

            if (HasErrorsInResponse(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        private void ValidateProductCart(ProductViewModel productCatalog, int quantity)
        {
            if (productCatalog == null) AddErrorValidation("Product does not exist!");
            if (quantity < 1) AddErrorValidation($"Choose at least one product unit {productCatalog.Name}");
            if (quantity > productCatalog.QuantityStock) AddErrorValidation($"The product {productCatalog.Name} has {productCatalog.QuantityStock} units in stock, you have selected {quantity}");
        }
    }
}