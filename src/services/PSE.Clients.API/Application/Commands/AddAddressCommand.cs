using FluentValidation;
using PSE.Core.Messages;
using System;

namespace PSE.Clients.API.Application.Commands;

public class AddAddressCommand : Command
{
    public Guid CustomerId { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string ZipCode { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public AddAddressCommand() { }

    public AddAddressCommand(Guid customerId, string street, string number, string complement, string zipCode, string neighborhood, string city, string state)
    {
        CustomerId = customerId;
        Street = street;
        Number = number;
        Complement = complement;
        ZipCode = zipCode;
        Neighborhood = neighborhood;
        City = city;
        State = state;
    }

    public override bool IsValid()
    {
        ValidationResult = new AddressValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class AddressValidation : AbstractValidator<AddAddressCommand>
    {
        public AddressValidation()
        {
            RuleFor(c => c.Street)
                .NotEmpty()
                .WithMessage("Inform the Street");

            RuleFor(c => c.Number)
                .NotEmpty()
                .WithMessage("Inform the Number");

            RuleFor(c => c.ZipCode)
                .NotEmpty()
                .WithMessage("Inform the ZipCode");

            RuleFor(c => c.Neighborhood)
                .NotEmpty()
                .WithMessage("Inform the Neighborhood");

            RuleFor(c => c.City)
                .NotEmpty()
                .WithMessage("Inform the City");

            RuleFor(c => c.State)
                .NotEmpty()
                .WithMessage("Inform the State");
        }
    }
}