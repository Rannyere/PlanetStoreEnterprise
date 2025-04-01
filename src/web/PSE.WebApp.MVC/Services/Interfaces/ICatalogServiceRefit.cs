using PSE.WebApp.MVC.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Services.Interfaces;

public interface ICatalogServiceRefit
{
    [Get("/catalog/products/")]
    Task<IEnumerable<ProductViewModel>> GetAllProducts();

    [Get("/catalog/products/{id}")]
    Task<ProductViewModel> GetProductById(Guid id);
}