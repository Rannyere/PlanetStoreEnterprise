using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PSE.Clients.API.Application.Events;
using PSE.Clients.API.Models;
using PSE.Core.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PSE.Clients.API.Application.Commands;

public class CustomerCommandHandler : CommandHandler,
    IRequestHandler<CustomerRegisterCommand, ValidationResult>,
    IRequestHandler<AddAddressCommand, ValidationResult>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ValidationResult> Handle(CustomerRegisterCommand message, CancellationToken cancellationToken)
    {
        try
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Customer(message.Id, message.Name, message.Email, message.Cpf);

            var customerExist = await _customerRepository.GetByCpf(customer.Cpf.Number);

            if (customerExist != null)
            {
                AddErrors("CPF is already in use");
                return ValidationResult;
            }

            _customerRepository.Add(customer);

            customer.AddEvent(new CustomerRegisteredEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await PersistToBase(_customerRepository.UnitOfWork);
        }
        catch (DbUpdateException dbEx)
        {
            var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            AddErrors($"Database error: {innerMessage}");
            return ValidationResult;
        }
        catch (Exception ex)
        {
            var innerMessage = ex.InnerException?.Message ?? ex.Message;
            AddErrors($"Error registering customer: {innerMessage}");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(AddAddressCommand message, CancellationToken cancellationToken)
    {
        try
        {
            if (!message.IsValid()) return message.ValidationResult;

            var address = new Address(message.Street, message.Number, message.Complement, message.ZipCode,
                message.Neighborhood, message.City, message.State, message.CustomerId);

            _customerRepository.AddAddress(address);

            return await PersistToBase(_customerRepository.UnitOfWork);
        }

        catch (DbUpdateException dbEx)
        {
            var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            AddErrors($"Database error: {innerMessage}");
            return ValidationResult;
        }
        catch (Exception ex)
        {
            var innerMessage = ex.InnerException?.Message ?? ex.Message;
            AddErrors($"Error adding address: {innerMessage}");
            return ValidationResult;
        }
    }
}
