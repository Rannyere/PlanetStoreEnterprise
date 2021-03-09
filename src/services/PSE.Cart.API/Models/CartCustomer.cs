using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
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
        public ValidationResult ValidationResult { get; set; }

        public bool VoucherUsage { get; set; }
        public decimal Discount { get; set; }

        public Voucher Voucher { get; set; }


        public CartCustomer(Guid customerId) 
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        public CartCustomer() { }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUsage = true;
            CalculateTotalCart();
        }

        internal void CalculateTotalCart()
        {
            TotalValue = Items.Sum(p => p.CalculateTotalByItem());
            CalculateValueTotalDiscount();
        }

        private void CalculateValueTotalDiscount()
        {
            if (!VoucherUsage) return;

            decimal discount = 0;
            var value = TotalValue;

            if (Voucher.DiscountType == DiscountTypeVoucher.Percentage)
            {
                if (Voucher.DiscountPercentage.HasValue)
                {
                    discount = (value * Voucher.DiscountPercentage.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            TotalValue = value < 0 ? 0 : value;
            Discount = discount;
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

        internal void UpdateExistingItem(CartItem item)
        {
            item.LinkCart(Id);

            var itemExisting = GetProductById(item.ProductId);

            Items.Remove(itemExisting);
            Items.Add(item);

            CalculateTotalCart();
        }

        internal void UpdateUnits(CartItem item, int units)
        {
            item.UpdateUnits(units);
            UpdateExistingItem(item);
        }

        internal void RemoveItem(CartItem item)
        {
            Items.Remove(GetProductById(item.ProductId));
            CalculateTotalCart();
        }

        internal bool IsValid()
        {
            var errors = Items.SelectMany(i => new CartItem.CartItemValidation().Validate(i).Errors).ToList();
            errors.AddRange(new CartCustomerValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;
        }

        public class CartCustomerValidation : AbstractValidator<CartCustomer>
        {
            public CartCustomerValidation()
            {
                RuleFor(c => c.CustomerId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Customer not found");

                RuleFor(c => c.Items.Count)
                    .GreaterThan(0)
                    .WithMessage("The cart has no items");

                RuleFor(c => c.TotalValue)
                    .GreaterThan(0)
                    .WithMessage("Total cart value must be greater than 0");
            }
        }
    }
}
