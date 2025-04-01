using FluentValidation;
using PSE.Core.Messages;
using PSE.Core.Utils;
using System;

namespace PSE.Clients.API.Application.Commands;

public class CustomerRegisterCommand : Command
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    private string _cpf;
    public string Cpf
    {
        get => _cpf;
        private set => _cpf = value?.OnlyNumbers(value);
    }

    public CustomerRegisterCommand(Guid id, string name, string email, string cpf)
    {
        AggregatedId = id;
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public override bool IsValid()
    {
        ValidationResult = new CustomerRegisterValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class CustomerRegisterValidation : AbstractValidator<CustomerRegisterCommand>
{
    public CustomerRegisterValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Id Customer invalid");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("The Customer name was not informed");

        RuleFor(c => c.Cpf)
            .NotEmpty()
            .WithMessage("The CPF was not informed")
            .Must(HasCpfValid)
            .WithMessage("The CPF informed is not valid.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("The Email was not informed")
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