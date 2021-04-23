using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PSE.Sales.BFF.DTOs
{
    public class OrderDTO
    {
        #region Order
        public int Code { get; set; }
        public int OrderStatus { get; set; }
        public DateTime DateRegister { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }
        public bool VoucherUsage { get; set; }

        public List<ItemCartDTO> OrderItems { get; set; } = new List<ItemCartDTO>();

        #endregion

        #region Address

        public AddressDTO Address { get; set; }

        #endregion

        #region Card

        [Required(ErrorMessage = "Enter the card number")]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Enter the name of the card holder")]
        [DisplayName("Card Holder")]
        public string CardHolder { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "The expiration of the card must be in the MM/AA format")]
        [Required(ErrorMessage = "Enter the expiration")]
        [DisplayName("Due date MM/AA")]
        public string CardExpiration { get; set; }

        [Required(ErrorMessage = "Enter the security code")]
        [DisplayName("Security Code")]
        public string CardCvv { get; set; }

        #endregion
    }
}
