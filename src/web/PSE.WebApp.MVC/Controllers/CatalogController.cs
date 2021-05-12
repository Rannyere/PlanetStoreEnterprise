using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Services.Interfaces;

namespace PSE.WebApp.MVC.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("showcase")]
        public async Task<IActionResult> Index([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var products = await _catalogService.GetAllProducts(ps, page, q);
            ViewBag.Search = q;
            products.ReferenceAction = "Index";

            return View(products);
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            var product = await _catalogService.GetProductById(id);

            return View(product);
        }
    }
}
