using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Services.Interfaces;

namespace PSE.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ISalesBffService _salesService;

        public CartViewComponent(ISalesBffService salesService)
        {
            _salesService = salesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _salesService.GetQuantityProductsInCart());
        }
    }
}
