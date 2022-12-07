namespace API.Interfaces;
public interface IOrderService
{
    Task<OrderEntity> CreateOrderAsync(
        string buyerEmail, 
        int deliveryMethodId, 
        string basketId, 
        AddressOwned shippingToAddress,
        CancellationToken cancellationToken);
    Task<IReadOnlyList<OrderEntity>> GetOrdersForUserAsync(string buyerEmail, CancellationToken cancellationToken);
    Task<OrderEntity> GetOrderByIdAsync(int id, string buyerEmail, CancellationToken cancellationToken);

    //Will try to move it away from here
    Task<IReadOnlyList<DeliveryMethodEntity>> GetDeliveryMethodsAsync(CancellationToken cancellationToken);
}
