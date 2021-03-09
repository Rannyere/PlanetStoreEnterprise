using System;
using System.Collections.Generic;

namespace PSE.Sales.BFF.DTOs
{
    public class CartCustomerDTO
    {
        public decimal TotalValue { get; set; }
        public VoucherDTO Voucher { get; set; }
        public bool VoucherUsage { get; set; }
        public decimal Discount { get; set; }
        public List<ItemCartDTO> Items { get; set; } = new List<ItemCartDTO>();
    }
}
