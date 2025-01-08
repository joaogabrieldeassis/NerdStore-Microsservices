namespace NerdStoreEnterprise.Order.Api.Application.CQRS.DTOs;

public class VoucherDTO
{
    public string Code { get; set; } = string.Empty;
    public decimal? Percentage { get; set; }
    public decimal? DiscountValue { get; set; }
    public int DiscountType { get; set; }
}
