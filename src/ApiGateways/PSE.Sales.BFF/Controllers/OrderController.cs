using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSE.Sales.BFF.DTOs;
using PSE.Sales.BFF.Services;
using PSE.WebAPI.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PSE.Sales.BFF.Controllers;

[Authorize]
public class OrderController : MainController
{
    private readonly ICartService _cartService;
    private readonly ICatalogService _catalogService;
    private readonly IOrderService _orderService;
    private readonly ICustomerService _customerService;

    public OrderController(ICartService cartService,
                          ICatalogService catalogService,
                          IOrderService orderService,
                          ICustomerService customerService)
    {
        _cartService = cartService;
        _catalogService = catalogService;
        _orderService = orderService;
        _customerService = customerService;
    }

    [HttpPost]
    [Route("sales/order")]
    public async Task<IActionResult> AddOrder(OrderDTO order)
    {
        var cartCustomer = await _cartService.GetCart();
        var products = await _catalogService.GetItems(cartCustomer.Items.Select(p => p.ProductId));
        var address = await _customerService.GetAddress();

        if (!await ValidateCartItems(cartCustomer, products)) return CustomResponse();

        PopulateDataOrder(cartCustomer, address, order);

        return CustomResponse(await _orderService.Checkout(order));
    }

    [HttpGet("sales/order/last")]
    public async Task<IActionResult> LastOrder()
    {
        var order = await _orderService.GetLastOrder();
        if (order is null)
        {
            AddErrorInProcess("Order not found!");
            return CustomResponse();
        }

        return CustomResponse(order);
    }

    [HttpGet("sales/order/list")]
    public async Task<IActionResult> ListOrdersByCustomer()
    {
        var orders = await _orderService.GetListOrdersByCustomerId();

        return orders == null ? NotFound() : CustomResponse(orders);
    }

    private async Task<bool> ValidateCartItems(CartCustomerDTO cart, IEnumerable<ProductCatalogDTO> products)
    {
        if (cart.Items.Count != products.Count())
        {
            var unavailableItems = cart.Items.Select(c => c.ProductId).Except(products.Select(p => p.Id)).ToList();

            foreach (var itemId in unavailableItems)
            {
                var itemCart = cart.Items.FirstOrDefault(c => c.ProductId == itemId);
                AddErrorInProcess($"The item {itemCart.Name} is no longer available in the catalog, please remove it from the cart to proceed with the purchase");
            }

            return false;
        }

        foreach (var itemCart in cart.Items)
        {
            var productCatalog = products.FirstOrDefault(p => p.Id == itemCart.ProductId);

            if (productCatalog.Value != itemCart.Value)
            {
                var msgErro = $"The product {itemCart.Name} has changed its value (from:" +
                              $"{string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", itemCart.Value)} to: " +
                              $"{string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", productCatalog.Value)}) since it was added to the cart.";

                AddErrorInProcess(msgErro);

                var responseRemover = await _cartService.RemoveProductCart(itemCart.ProductId);
                if (ResponseHasErrors(responseRemover))
                {
                    AddErrorInProcess($"It was not possible to automatically remove the product {itemCart.Name} from your cart, _" + " remove and add again if you still want to buy this item");
                    return false;
                }

                itemCart.Value = productCatalog.Value;
                var responseAdd = await _cartService.AddProductCart(itemCart);

                if (ResponseHasErrors(responseAdd))
                {
                    AddErrorInProcess($"It was not possible to automatically update the product {itemCart.Name} in your cart, _" + "add again if you still want to buy this item");
                    return false;
                }

                ClearErrorsProcess();
                AddErrorInProcess(msgErro + " We updated the value in your cart, check the order and if you prefer, remove the product");
                return false;
            }
        }

        return true;
    }

    private void PopulateDataOrder(CartCustomerDTO cartCustomer, AddressDTO address, OrderDTO order)
    {
        order.VoucherCode = cartCustomer.Voucher?.Code;
        order.VoucherUsage = cartCustomer.VoucherUsage;
        order.TotalValue = cartCustomer.TotalValue;
        order.Discount = cartCustomer.Discount;
        order.OrderItems = cartCustomer.Items;

        order.Address = address;
    }
}