﻿using System;
using PSE.Core.DomainObjects;

namespace PSE.Client.API.Models
{
    public class Address : Entity
    {
        public string Street { get; private set; }

        public string Number { get; private set; }

        public string Complement { get; private set; }

        public string ZipCode { get; private set; }

        public string Neighborhood { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public Guid ClientId { get; private set; }

        /* Entity Framework Relations */
        public Client Client { get; protected set; }

        public Address(string street, string number, string complement, string zipCode, string neighborhood, string city, string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            ZipCode = zipCode;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }
    }
}
