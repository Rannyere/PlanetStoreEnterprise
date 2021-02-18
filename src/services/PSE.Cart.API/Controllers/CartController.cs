using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSE.Cart.API.Models;
using PSE.WebAPI.Core.Controllers;
using PSE.WebApp.Core.User;

namespace PSE.Cart.API.Controllers
{
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;

        public CartController(IAspNetUser user)
        {
            _user = user;
        }

        [HttpGet("cart")]
        public async Task<CartCustomer> GetCart()
        {
            return null;
        }

        [HttpPost("cart")]
        public async Task<IActionResult> UpdateCartItem(CartItem item)
        {
            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, CartItem item)
        {
            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> RemoveCartItem(Guid produtoId)
        {
            return CustomResponse();
        }
    }
}