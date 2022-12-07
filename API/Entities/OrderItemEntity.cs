namespace API.Entities;
public sealed class OrderItemEntity : BaseEntity
{
    public ProductItemOrderedOwned ItemOrdered { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public int OrderId { get; set; }
}