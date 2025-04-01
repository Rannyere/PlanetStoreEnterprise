using PSE.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace PSE.Clients.API.Models;

public class Customer : Entity, IAggregatedRoot
{
    public string Name { get; private set; }

    public Email Email { get; private set; }

    public Cpf Cpf { get; private set; }

    public bool Removed { get; private set; }

    public IEnumerable<Address> Addresses { get; private set; }

    /* Entity Framework Relation */
    protected Customer() { }

    public Customer(Guid id, string name, string email, string cpf)
    {
        Id = id;
        Name = name;
        Email = new Email(email);
        Cpf = new Cpf(cpf);
        Removed = false;
    }

    public void ChangeEmail(string email)
    {
        Email = new Email(email);
    }

    public void AttributeAddress(IEnumerable<Address> addresses)
    {
        Addresses = addresses;
    }
}