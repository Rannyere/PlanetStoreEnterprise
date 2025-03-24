namespace PSE.Order.API.Application.DTOs;

public class VoucherDTO
{
    public string Code { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public decimal? DiscountValue { get; set; }
    public int DiscountType { get; set; }
}