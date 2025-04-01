using PSE.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSE.Clients.API.Models;

public interface ICustomerRepository : IRepository<Customer>
{
    void Add(Customer customer);

    Task<IEnumerable<Customer>> GetAll();

    Task<Customer> GetByCpf(string cpf);

    Task<Address> GetAddressById(Guid id);

    void AddAddress(Address address);
}