using System;
using System.Collections.Generic;
using PSE.Core.DomainObjects;

namespace PSE.Payment.API.Models
{
    public class PaymentInfo : Entity, IAggregatedRoot
    {
        public Guid OrderId { get; set; }
        public PaymentMehtod PaymentMehtod { get; set; }
        public decimal TotalValue { get; set; }

        public CreditCard CreditCard { get; set; }

        // EF Relation
        public ICollection<Transaction> Transactions { get; set; }

        public PaymentInfo()
        {
            Transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
