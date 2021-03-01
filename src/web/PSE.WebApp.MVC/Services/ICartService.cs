using System;
using System.Threading.Tasks;
using PSE.Core.Responses;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Services
{
    public interface ICartService
    {
        Task<CartCustomerViewModel> GetCart();
        Task<ResponseErrorResult> AddProductCart(CartProductViewModel product);
        Task<ResponseErrorResult> UpdateProductCart(Guid productId, CartProductViewModel product);
        Task<ResponseErrorResult> RemoveProductCart(Guid productId);
    }
}
