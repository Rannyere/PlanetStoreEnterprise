using System;
namespace PSE.Payment.API.Models
{
    public enum StatusTransaction
    {
        Authorized = 1,
        Paid,
        Denied,
        Refunded,
        Canceled
    }
}
