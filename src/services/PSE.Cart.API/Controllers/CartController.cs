using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSE.Cart.API.Data;
using PSE.Cart.API.Models;
using PSE.WebAPI.Core.Controllers;
using PSE.WebAPI.Core.User;

namespace PSE.Cart.API.Controllers
{
    [Authorize]
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
            var cart = await GetCartCustomer();

            if (cart == null)
                ManipulateNewCart(item);
            else
                ManipulateExistingCart(cart, item);

            if (!ValidOperation()) return CustomResponse();

            await PersistBase();

            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, CartItem item)
        {
            var cart = await GetCartCustomer();
            var itemCart = await ValidateItem(productId, cart, item);
            if (itemCart == null) return CustomResponse();

            cart.UpdateUnits(itemCart, item.Quantity);

            ValidateCart(cart);
            if (ValidOperation()) return CustomResponse();

            _cartDbContext.CartCustomers.Update(cart);
            _cartDbContext.CartItems.Update(itemCart);

            await PersistBase();

            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var cart = await GetCartCustomer();
            var itemCart = await ValidateItem(productId, cart);
            if (itemCart == null) return CustomResponse();

            cart.RemoveItem(itemCart);

            ValidateCart(cart);
            if (ValidOperation()) return CustomResponse();

            _cartDbContext.CartItems.Remove(itemCart);
            _cartDbContext.CartCustomers.Update(cart);

            await PersistBase();

            return CustomResponse();
        }

        private void ManipulateNewCart(CartItem item)
        {
            var cart = new CartCustomer(_user.GetUserId());
            cart.AddNewItem(item);

            ValidateCart(cart);
            _cartDbContext.CartCustomers.Add(cart);
        }

        private void ManipulateExistingCart(CartCustomer cart, CartItem item)
        {
            var productExisting = cart.CartItemExisting(item);

            cart.AddNewItem(item);
            ValidateCart(cart);

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

        private async Task<CartItem> ValidateItem(Guid productId, CartCustomer cart, CartItem item = null)
        {
            if (item != null && productId != item.ProductId)
            {
                AddErrorInProcess("The item does not correspond to the informed");
                return null;
            }

            if (cart == null)
            {
                AddErrorInProcess("Cart not found");
                return null;
            }

            var itemCart = await _cartDbContext.CartItems
                .FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);

            if (itemCart == null || !cart.CartItemExisting(itemCart))
            {
                AddErrorInProcess("The item is not in the cart");
                return null;
            }

            return itemCart;
        }

        private bool ValidateCart(CartCustomer cart)
        {
            if (cart.IsValid()) return true;

            cart.ValidationResult.Errors.ToList().ForEach(e => AddErrorInProcess(e.ErrorMessage));
            return false;
        }
    }
}