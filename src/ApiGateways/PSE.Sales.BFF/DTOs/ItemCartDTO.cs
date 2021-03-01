using System;
namespace PSE.Sales.BFF.DTOs
{
    public class ItemCartDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
