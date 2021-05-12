using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSE.Core.Data;

namespace PSE.Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResult<Product>> GetAll(int pageSize, int pageIndex, string query = null);

        Task<Product> GetById(Guid idProduct);

        Task<List<Product>> GetPoductsById(string ids);

        void Add(Product product);

        void Update(Product product);
    }
}
