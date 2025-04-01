namespace PSE.Payment.Gateway;

public enum TransactionStatus
{
    Authorized = 1,
    Paid,
    Refused,
    Chargedback,
    Cancelled
}