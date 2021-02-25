using System;
using System.Text.Json.Serialization;
using FluentValidation;

namespace PSE.Cart.API.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }

        public Guid CartId { get; set; }

        /* Entity Framework Relation */
        [JsonIgnore]
        public CartCustomer CartCustomer { get; set; }

        public CartItem( )
        {
            Id = Guid.NewGuid();
        }

        internal void LinkCart(Guid cartId)
        {
            CartId = cartId;
        }

        internal decimal CalculateTotalByItem()
        {
            return Quantity * Value;
        }

        internal void AddUnits(int units)
        {
            Quantity += units;
        }

        internal void UpdateUnits(int units)
        {
            Quantity = units;
        }

        internal bool IsValid()
        {
            return new CartItemValidation().Validate(this).IsValid;
        }

        public class CartItemValidation : AbstractValidator<CartItem>
        {
            public CartItemValidation()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Product Id invalid");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("The product name was not provided");

                RuleFor(c => c.Quantity)
                    .GreaterThan(0)
                    .WithMessage(item => $"The minimum order quantity for {item.Name} is 1");

                RuleFor(c => c.Quantity)
                    .LessThanOrEqualTo(CartCustomer.MAX_QUANTITY_ITEM)
                    .WithMessage(item => $"The maximum order quantity for {item.Name} is {CartCustomer.MAX_QUANTITY_ITEM}");

                RuleFor(c => c.Value)
                    .GreaterThan(0)
                    .WithMessage(item => $"{item.Name} must be greater than 0 ");
            }
        }
    }
}
