namespace API.DTOs;
public sealed class OrderToReturnDto
{
    public int Id { get; set; }
    public string BuyerEmail { get; set; }
    public string DeliveryMethod { get; set; }
    public DateTimeOffset OrderDate { get; set; } 
    public string Status { get; set; }
    public AddressOwned ShipToAddress { get; set; }
    public decimal ShippingPrice { get; set; }
    public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
    public decimal Total { get; set; }
}
