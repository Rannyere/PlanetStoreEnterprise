using System;
using System.Threading.Tasks;
using PSE.Core.Responses;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Services.Interfaces
{
    public interface ISalesBffService
    {
        // CART
        Task<CartCustomerViewModel> GetCart();
        Task<int> GetQuantityProductsInCart();
        Task<ResponseErrorResult> AddProductCart(ItemCartViewModel itemCart);
        Task<ResponseErrorResult> UpdateProductCart(Guid productId, ItemCartViewModel itemCart);
        Task<ResponseErrorResult> RemoveProductCart(Guid productId);
        Task<ResponseErrorResult> ApplyVoucherCart(string voucher);
    }
}
