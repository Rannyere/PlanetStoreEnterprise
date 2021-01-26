﻿using System;
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
            return base.IsValid();
        }
    }
}
