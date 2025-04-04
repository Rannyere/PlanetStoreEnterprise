using PSE.Core.DomainObjects;
using System;

namespace PSE.Payment.API.Models;

public class Transaction : Entity
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
    public PaymentInfo Payment { get; set; }
}