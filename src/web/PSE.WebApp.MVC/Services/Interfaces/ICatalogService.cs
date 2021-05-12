using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<PagedViewModel<ProductViewModel>> GetAllProducts(int pageSize, int pageIndex, string query = null);

        Task<ProductViewModel> GetProductById(Guid id);
    }

}
