using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSE.Core.Data;

namespace PSE.Clients.API.Models
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Add(Customer customer);

        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> GetByCpf(string cpf);
    }
}
