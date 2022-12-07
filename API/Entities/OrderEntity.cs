namespace API.Entities;
public sealed class OrderEntity : BaseEntity
{
    public string? BuyerEmail { get; set; }
    public DeliveryMethodEntity DeliveryMethod { get; set; }
    public DateTimeOffset? OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderStatusEnum? Status { get; set; } = OrderStatusEnum.Pending;
    public AddressOwned ShipToAddress { get; set; }
    public IReadOnlyList<OrderItemEntity> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public string? PaymentIntentId { get; set; }

    public decimal GetTotal()
    {
        return Subtotal + DeliveryMethod.Price;
    }

}
