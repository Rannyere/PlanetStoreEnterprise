using System;
namespace PSE.Payment.API.Models
{
    public class Transaction
    {
        public string AuthorizationCode { get; set; }
        public string FlagCard { get; set; }
        public DateTime? DateTransaction { get; set; }
        public decimal TotalValue { get; set; }
        public decimal CostTransaction { get; set; }
        public StatusTransaction Status { get; set; }
        public string TID { get; set; } // Id transaction
        public string NSU { get; set; } // Meio (ex:paypal)

        public Guid PaymentId { get; set; }

        // EF Relation
        public Payment Payment { get; set; }
    }
}
