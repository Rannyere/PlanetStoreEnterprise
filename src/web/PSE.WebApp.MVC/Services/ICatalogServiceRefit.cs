using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSE.WebApp.MVC.Models;
using Refit;

namespace PSE.WebApp.MVC.Services
{
    public interface ICatalogServiceRefit
    {
        [Get("/catalog/products/")]
        Task<IEnumerable<ProductViewModel>> GetAllProducts();

        [Get("/catalog/products/{id}")]
        Task<ProductViewModel> GetProductById(Guid id);
    }
}
