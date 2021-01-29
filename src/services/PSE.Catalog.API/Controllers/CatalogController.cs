﻿using System;
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
        public async Task<IEnumerable<Product>> Index()
        {
            return await _productRepository.GetAll();
        }

        [ClaimsAuthorize("Catalog", "Read")]
        [HttpGet("catalog/products/{id}")]
        public async Task<Product> ProductDetail(Guid id)
        {
            return await _productRepository.GetById(id);
        }
    }
}
