using FluentValidation;
using PSE.Core.Messages;
using PSE.Order.API.Application.DTOs;
using System;
using System.Collections.Generic;

namespace PSE.Order.API.Application.Commands;

public class ExecuteOrderCommand : Command
{
    // Order
    public Guid CustomerId { get; set; }
    public decimal TotalValue { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }

    // Voucher
    public string VoucherCode { get; set; }
    public bool VoucherUsage { get; set; }
    public decimal Discount { get; set; }

    // Address
    public AddressDTO Address { get; set; }

    // Card
    public string CardNumber { get; set; }
    public string CardHolder { get; set; }
    public string CardExpiration { get; set; }
    public string CardCvv { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new ExecuteOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class ExecuteOrderValidation : AbstractValidator<ExecuteOrderCommand>
    {
        public ExecuteOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid customer id");

            RuleFor(c => c.OrderItems.Count)
                .GreaterThan(0)
                .WithMessage("Order must have at least 1 item");

            RuleFor(c => c.TotalValue)
                .GreaterThan(0)
                .WithMessage("Invalid order value");

            RuleFor(c => c.CardNumber)
                .CreditCard()
                .WithMessage("Invalid card number");

            RuleFor(c => c.CardHolder)
                .NotNull()
                .WithMessage("Name of cardholder required.");

            RuleFor(c => c.CardCvv.Length)
                .GreaterThan(2)
                .LessThan(5)
                .WithMessage("The CVV of the card must have 3 or 4 numbers.");

            RuleFor(c => c.CardExpiration)
                .NotNull()
                .WithMessage("Card expiration date required.");
        }
    }
}