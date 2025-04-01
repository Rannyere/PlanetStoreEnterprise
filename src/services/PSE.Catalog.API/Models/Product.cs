using PSE.Core.DomainObjects;
using System;

namespace PSE.Catalog.API.Models;

public class Product : Entity, IAggregatedRoot
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Activ { get; set; }
    public decimal Value { get; set; }
    public DateTime DateRegister { get; set; }
    public string Image { get; set; }
    public int QuantityStock { get; set; }

    public void RemoveStock(int quantity)
    {
        if (QuantityStock >= quantity)
            QuantityStock -= quantity;
    }

    public bool IsAvailable(int quantity)
    {
        return Activ && QuantityStock >= quantity;
    }
}