namespace API.Services;
public sealed class OrderService : IOrderService
{
    private readonly IRepository<DeliveryMethodEntity> _deliveryMethodRepository;
    private readonly IRepository<OrderEntity> _orderRepository;
    private readonly IRepository<ProductEntity> _productRepository;
    private readonly IBasketRepository _basketRepository;

    public OrderService(
        IRepository<DeliveryMethodEntity> deliveryMethodRepository,
        IRepository<OrderEntity> orderRepository,
        IRepository<ProductEntity> productRepository,
        IBasketRepository basketRepository)
    {
        _deliveryMethodRepository = deliveryMethodRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _basketRepository = basketRepository;
    }

    public async Task<OrderEntity> CreateOrderAsync(
        string buyerEmail, int deliveryMethodId,
        string basketId, AddressOwned shippingToAddress,
        CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetBasketByIdAsync(basketId);

        var items = new List<OrderItemEntity>();

        foreach (var item in basket.Items)
        {
            var productItem = await _productRepository.GetByIdAsync(item.Id, cancellationToken);
            var itemOrderd = new ProductItemOrderedOwned
            {
                ProductItemId = productItem.Id,
                PictureUrl = productItem.PictureUrl,
                ProductName = productItem.Name
            };
            var orderItem = new OrderItemEntity
            {
                ItemOrdered = itemOrderd,
                Price = productItem.Price,
                Quantity = item.Quantity
            };

            items.Add(orderItem);
        }

        var deliveryMethod = await _deliveryMethodRepository.GetByIdAsync(deliveryMethodId, cancellationToken);

        var subtotal = items.Sum(_ => _.Price * _.Quantity);

        var order = new OrderEntity
        {
            BuyerEmail = buyerEmail,
            ShipToAddress = shippingToAddress,
            DeliveryMethod = deliveryMethod,
            OrderItems = items,
            Subtotal = subtotal
        };

        await _orderRepository.Add(order, cancellationToken);

        await _basketRepository.DeleteBasketAsync(basketId);

        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethodEntity>> GetDeliveryMethodsAsync(
        CancellationToken cancellationToken)
    {
        return await _deliveryMethodRepository.GetAllAsync(cancellationToken);
    }

    public async Task<OrderEntity> GetOrderByIdAsync(
        int id, string buyerEmail, CancellationToken cancellationToken)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(id, buyerEmail);

        return await _orderRepository.GetEntityWithSpecAsync(spec, cancellationToken);
    }

    public async Task<IReadOnlyList<OrderEntity>> GetOrdersForUserAsync(
        string buyerEmail, CancellationToken cancellationToken)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);

        return await _orderRepository.GetEntitiesWithSpecAsync(spec, cancellationToken);
    }
}
