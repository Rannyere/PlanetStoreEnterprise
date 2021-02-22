using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSE.Cart.API.Data;
using PSE.Cart.API.Models;
using PSE.WebAPI.Core.Controllers;
using PSE.WebApp.Core.User;

namespace PSE.Cart.API.Controllers
{
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly CartDbContext _cartDbContext;

        public CartController(IAspNetUser user,
                              CartDbContext cartDbContext)
        {
            _user = user;
            _cartDbContext = cartDbContext;
        }

        [HttpGet("cart")]
        public async Task<CartCustomer> GetCart()
        {
            return await GetCartCustomer() ?? new CartCustomer();
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddCartItem(CartItem item)
        {
            var cartCustomer = await GetCartCustomer();

            if (cartCustomer == null)
                ManipulateNewCart(item);
            else
                ManipulateExistingCart(cartCustomer, item);

            if (ValidOperation()) return CustomResponse();

            await PersistBase();

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

        private void ManipulateNewCart(CartItem item)
        {
            var cart = new CartCustomer(_user.GetUserId());
            cart.AddNewItem(item);

            _cartDbContext.CartCustomers.Add(cart);
        }

        private void ManipulateExistingCart(CartCustomer cart, CartItem item)
        {
            var productExisting = cart.CartItemExisting(item);

            cart.AddNewItem(item);

            if (productExisting)
            {
                _cartDbContext.CartItems.Update(cart.GetProductById(item.ProductId));
            }
            else
            {
                _cartDbContext.CartItems.Add(item);
            }

            _cartDbContext.CartCustomers.Update(cart);
        }

        private async Task<CartCustomer> GetCartCustomer()
        {
            return await _cartDbContext.CartCustomers
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());
        }

        private async Task PersistBase()
        {
            var result = await _cartDbContext.SaveChangesAsync();
            if (result <= 0) AddErrorInProcess("Could not persist data");
        }
    }
}