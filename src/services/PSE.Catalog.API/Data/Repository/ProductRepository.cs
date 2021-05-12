using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PSE.Catalog.API.Models;
using PSE.Core.Data;

namespace PSE.Catalog.API.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public async Task<PagedResult<Product>> GetAll(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT * FROM Products 
                      WHERE (@Search IS NULL OR Name LIKE @Search) 
                      ORDER BY Name 
                      LIMIT {pageSize * (pageIndex - 1)}, {pageSize};   
                      SELECT COUNT(Id) FROM Products 
                      WHERE (@Search IS NULL OR Name LIKE @Search);";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Search = "%" + query + "%" });

            var products = multi.Read<Product>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Product>()
            {
                List = products,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

        public async Task<Product> GetById(Guid idProduct)
        {
            return await _context.Products.FindAsync(idProduct);
        }

        public async Task<List<Product>> GetPoductsById(string ids)
        {
            var idsGuid = ids.Split(',')
                .Select(id => (Ok: Guid.TryParse(id, out var x), Value: x));

            if (!idsGuid.All(nid => nid.Ok)) return new List<Product>();

            var idsValue = idsGuid.Select(id => id.Value);

            return await _context.Products.AsNoTracking()
                .Where(p => idsValue.Contains(p.Id) && p.Activ).ToListAsync();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
