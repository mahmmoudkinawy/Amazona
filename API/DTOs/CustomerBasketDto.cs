namespace API.DTOs;
public sealed class CustomerBasketDto
{
    [Required]
    public string Id { get; set; }

    public List<BasketItemDto> Items { get; set; } = new();

    public int? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
}
