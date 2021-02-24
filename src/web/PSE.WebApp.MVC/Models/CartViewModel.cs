using System;
using System.Collections.Generic;

namespace PSE.WebApp.MVC.Models
{
    public class CartCustomerViewModel
    {
        public decimal TotalValue { get; set; }
        public List<CartProductViewModel> Items { get; set; } = new List<CartProductViewModel>();
    }

    public class CartProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
    }
}
