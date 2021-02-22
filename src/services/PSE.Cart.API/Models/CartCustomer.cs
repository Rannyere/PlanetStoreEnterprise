using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace PSE.Cart.API.Models
{
    public class CartCustomer
    {
        internal const int MAX_QUANTITY_ITEM = 5;

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalValue { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public CartCustomer(Guid customerId) 
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        public CartCustomer() { }

        internal void CalculateTotalCart()
        {
            TotalValue = Items.Sum(p => p.CalculateTotalByItem());
        }

        internal bool CartItemExisting(CartItem item)
        {
            return Items.Any(p => p.ProductId == item.ProductId);
        }

        internal CartItem GetProductById(Guid produtoId)
        {
            return Items.FirstOrDefault(p => p.ProductId == produtoId);
        }

        internal void AddNewItem(CartItem item)
        {
            if (!item.IsValid()) return;

            item.LinkCart(Id);

            if (CartItemExisting(item))
            {
                var itemExisting = GetProductById(item.ProductId);
                itemExisting.AddUnits(item.Quantity);

                item = itemExisting;
                Items.Remove(itemExisting);
            }

            Items.Add(item);
            CalculateTotalCart();
        }
    }
}
