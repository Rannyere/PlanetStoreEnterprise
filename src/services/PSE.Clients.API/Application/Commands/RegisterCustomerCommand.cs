using System;
using FluentValidation;
using PSE.Core.Messages;

namespace PSE.Clients.API.Application.Commands
{
    public class RegisterCustomerCommand : Command
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Cpf { get; private set; }

        public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregatedId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id Customer invalid");

            RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("The Customer name was not informed");

            RuleFor(c => c.Cpf)
                    .Must(HasCpfValid)
                    .WithMessage("The CPF informed is not valid.");

            RuleFor(c => c.Email)
                .Must(HasEmailValid)
                .WithMessage("The E-mail informed is not valid.");
        }

        protected static bool HasCpfValid(string cpf)
        {
            return Core.DomainObjects.Cpf.ValidateCpfBrazil(cpf);
        }

        protected static bool HasEmailValid(string email)
        {
            return Core.DomainObjects.Email.ValidateEmail(email);
        }
    }
}
