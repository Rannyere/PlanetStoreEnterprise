using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using PSE.Clients.API.Models;
using PSE.Core.Messages;

namespace PSE.Clients.API.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler, IRequestHandler<RegisterCustomerCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(RegisterCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Customer(message.Id, message.Name, message.Email, message.Cpf);

            //business validations

            if (true) // exemple add message error in case customer existing
            {
                AddErrors("CPF is already in use");
                return ValidationResult;
            }

            return message.ValidationResult;
        }
    }
}
