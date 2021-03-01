using System;
namespace PSE.Sales.BFF.DTOs
{
    public class ProductCatalogDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
    }
}
