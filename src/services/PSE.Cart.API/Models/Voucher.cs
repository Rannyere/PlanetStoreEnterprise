namespace PSE.Cart.API.Models;

public class Voucher
{
    public string Code { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public decimal? DiscountValue { get; set; }
    public DiscountTypeVoucher DiscountType { get; set; }
}

public enum DiscountTypeVoucher
{
    Percentage = 0,
    Value = 1

}