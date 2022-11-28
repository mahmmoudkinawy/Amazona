namespace API.Entities;
public sealed class CustomerBasketEntity
{
    public string Id { get; set; }
    public List<BasketItemEntity> Items { get; set; } = new();

    public CustomerBasketEntity(string id) => Id = id;

}
