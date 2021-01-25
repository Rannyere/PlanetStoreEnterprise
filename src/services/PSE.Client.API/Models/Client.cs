using System;
using PSE.Core.DomainObjects;

namespace PSE.Client.API.Models
{
    public class Client : Entity, IAggregatedRoot
    {
        public string Name { get; private set; }

        public Email Email { get; private set; }

        public Cpf Cpf { get; private set; }

        public bool Removed { get; private set; }

        public Address Address { get; private set; }

        /* Entity Framework Relation */
        protected Client() { }

        public Client(Guid id, string name, string email, string cpf)
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

        public void AttributeAddress(Address address)
        {
            Address = address;
        }
    }
}
