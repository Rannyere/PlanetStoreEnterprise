using PSE.Core.Responses;
using PSE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Services.Interfaces;

public interface ISalesBffService
{
    // CART
    Task<CartCustomerViewModel> GetCart();
    Task<int> GetQuantityProductsInCart();
    Task<ResponseErrorResult> AddProductCart(ItemCartViewModel itemCart);
    Task<ResponseErrorResult> UpdateProductCart(Guid productId, ItemCartViewModel itemCart);
    Task<ResponseErrorResult> RemoveProductCart(Guid productId);
    Task<ResponseErrorResult> ApplyVoucherCart(string voucher);

    // ORDER
    Task<ResponseErrorResult> Checkout(OrderTransactionViewModel orderTransaction);
    Task<OrderCustomerViewModel> GetLastOrder();
    Task<IEnumerable<OrderCustomerViewModel>> GetListOrdersByCustomerId();
    OrderTransactionViewModel MappingToOrder(CartCustomerViewModel cart, AddressViewModel address);
}