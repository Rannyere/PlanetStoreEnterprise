using System;
using System.Collections.Generic;

namespace PSE.WebApp.MVC.Models
{
    public class OrderCustomerViewModel
    {
        #region OrderCustomer

        public int Code { get; set; }
        public int OrderStatus { get; set; }
        public DateTime DateRegister { get; set; }
        public decimal TotalValue { get; set; }
        public bool VoucherUsage { get; set; }
        public decimal Discount { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();

        #endregion

        #region OrderItem

        public class OrderItemViewModel
        {
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Value { get; set; }
            public string Image { get; set; }
        }

        #endregion

        #region Address

        public AddressViewModel Address { get; set; }

        #endregion
    }
}