using Microsoft.EntityFrameworkCore;
using PSE.Clients.API.Models;
using PSE.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSE.Clients.API.Data.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly ClientsDbContext _context;

    public CustomerRepository(ClientsDbContext context)
    {
        _context = context;
    }

    public IUnityOfWork UnitOfWork => _context;

    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _context.Customers.AsNoTracking().ToListAsync();
    }

    public Task<Customer> GetByCpf(string cpf)
    {
        return _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public async Task<Address> GetAddressById(Guid id)
    {
        return await _context.Addresses.FirstOrDefaultAsync(a => a.CustomerId == id);
    }

    public void AddAddress(Address address)
    {
        _context.Addresses.Add(address);
    }
}