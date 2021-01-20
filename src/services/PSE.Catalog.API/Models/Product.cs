using System;
using PSE.Core.DomainObjects;

namespace PSE.Catalog.API.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Activ { get; set; }
        public decimal Value { get; set; }
        public DateTime DateRegister { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
    }
}
