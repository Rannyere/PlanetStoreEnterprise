using PSE.Core.Messages;
using System;

namespace PSE.Clients.API.Application.Events;

public class CustomerRegisteredEvent : Event
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string Cpf { get; private set; }

    public CustomerRegisteredEvent(Guid id, string name, string email, string cpf)
    {
        AggregatedId = id;
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }
}