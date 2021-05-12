using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSE.Catalog.API.Models;
using PSE.WebAPI.Core.Controllers;
using PSE.WebAPI.Core.Identification;

namespace PSE.Catalog.API.Controllers
{
    [Authorize]
    public class CatalogController : MainController
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet("catalog/products")]
        public async Task<PagedResult<Product>> Index([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return await _productRepository.GetAll(ps, page, q);
        }

        //[ClaimsAuthorize("Catalog", "Read")]
        [AllowAnonymous]
        [HttpGet("catalog/products/{id}")]
        public async Task<Product> ProductDetail(Guid id)
        {
            return await _productRepository.GetById(id);
        }

        [HttpGet("catalog/products/list/{ids}")]
        public async Task<IEnumerable<Product>> GetProductsById(string ids)
        {
            return await _productRepository.GetPoductsById(ids);
        }
    }
}
