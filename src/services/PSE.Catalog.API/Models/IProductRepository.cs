using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSE.Core.Data;

namespace PSE.Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> GetById(Guid idProduct);

        void Add(Product product);

        void Update(Product product);
    }
}
