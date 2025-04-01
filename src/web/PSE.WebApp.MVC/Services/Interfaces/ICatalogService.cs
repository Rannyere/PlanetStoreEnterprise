using PSE.WebApp.MVC.Models;
using System;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Services.Interfaces;

public interface ICatalogService
{
    Task<PagedViewModel<ProductViewModel>> GetAllProducts(int pageSize, int pageIndex, string query = null);

    Task<ProductViewModel> GetProductById(Guid id);
}
