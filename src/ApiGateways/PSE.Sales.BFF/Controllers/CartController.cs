using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSE.Sales.BFF.DTOs;
using PSE.Sales.BFF.Services;
using PSE.Sales.BFF.Services.gRPC;
using PSE.WebAPI.Core.Controllers;

namespace PSE.Sales.BFF.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICartGrpcService _cartGrpcService; //optional
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService,
                              ICartGrpcService cartGrpcService,
                              ICatalogService catalogService,
                              IOrderService orderService)
        {
            _cartService = cartService;
            _cartGrpcService = cartGrpcService;
            _catalogService = catalogService;
            _orderService = orderService;
        }

        [HttpGet]
        [Route("sales/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _cartService.GetCart());
        }

        [HttpGet]
        [Route("sales/cart-quantity")]
        public async Task<int> GetQuantityProductsInCart()
        {
            var quantity = await _cartService.GetCart();
            return quantity?.Items.Sum(i => i.Quantity) ?? 0;
        }

        [HttpPost]
        [Route("sales/cart/product")]
        public async Task<IActionResult> AddProductCart(ItemCartDTO itemCart)
        {
            var productCatalog = await _catalogService.GetProductById(itemCart.ProductId);

            await ValidateItemCart(productCatalog, itemCart.Quantity);
            if (!ValidOperation()) return CustomResponse();

            itemCart.Name = productCatalog.Name;
            itemCart.Value = productCatalog.Value;
            itemCart.Image = productCatalog.Image;

            var response = await _cartService.AddProductCart(itemCart);

            return CustomResponse(response);
        }

        [HttpPut]
        [Route("sales/cart/product/{productId}")]
        public async Task<IActionResult> UpdateProductCart(Guid productId, ItemCartDTO itemCart)
        {
            var productCatalog = await _catalogService.GetProductById(itemCart.ProductId);

            await ValidateItemCart(productCatalog, itemCart.Quantity);
            if (!ValidOperation()) return CustomResponse();

            var response = await _cartService.UpdateProductCart(productId, itemCart);

            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("sales/cart/product/{productId}")]
        public async Task<IActionResult> RemoveProductCart(Guid productId)
        {
            var productCatalog = await _catalogService.GetProductById(productId);

            if (productCatalog == null)
            {
                AddErrorInProcess("Product not found!");
                return CustomResponse();
            }

            var response = await _cartService.RemoveProductCart(productId);

            return CustomResponse(response);
        }

        [HttpPost]
        [Route("sales/cart/apply-voucher")]
        public async Task<IActionResult> ApplyVoucher([FromBody] string voucherCode)
        {
            var voucher = await _orderService.GetVoucherByCode(voucherCode);
            if (voucher is null)
            {
                AddErrorInProcess("Voucher invalid or not found!");
                return CustomResponse();
            }

            var response = await _cartService.ApplyVoucherCart(voucher);

            return CustomResponse(response);
        }

        private async Task ValidateItemCart(ProductCatalogDTO productCatalog, int quantity)
        {
            if (productCatalog == null) AddErrorInProcess("Product does not exist!");
            if (quantity < 1) AddErrorInProcess($"Choose at least one product unit {productCatalog.Name}");

            var cartCustomer = await _cartService.GetCart();
            var itemCart = cartCustomer.Items.FirstOrDefault(p => p.ProductId == productCatalog.Id);

            if (itemCart != null && itemCart.Quantity + quantity > productCatalog.QuantityStock)
            {
                AddErrorInProcess($"The product {productCatalog.Name} has {productCatalog.QuantityStock} units in stock, you have selected {quantity} ");
                return;
            }

            if (quantity > productCatalog.QuantityStock) AddErrorInProcess($"The product {productCatalog.Name} has {productCatalog.QuantityStock} units in stock, you have selected {quantity}");
        }
    }
}
