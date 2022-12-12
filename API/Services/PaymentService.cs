namespace API.Services;
public sealed class PaymentService : IPaymentService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IConfiguration _config;
    private readonly IRepository<ProductEntity> _productRepository;
    private readonly IRepository<DeliveryMethodEntity> _deliveryMethodRepository;

    public PaymentService(
        IBasketRepository basketRepository,
        IConfiguration config,
        IRepository<ProductEntity> productRepository,
        IRepository<DeliveryMethodEntity> deliveryMethodRepository)
    {
        _basketRepository = basketRepository;
        _config = config;
        _productRepository = productRepository;
        _deliveryMethodRepository = deliveryMethodRepository;
    }

    public async Task<CustomerBasketEntity> CreateOrUpdatePaymentIntent(
        string basketId, CancellationToken cancellationToken)
    {
        StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

        var basket = await _basketRepository.GetBasketByIdAsync(basketId);
        var shippingPrice = 0m;

        if (basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _deliveryMethodRepository
                .GetByIdAsync((int)basket.DeliveryMethodId, cancellationToken);
            shippingPrice = deliveryMethod.Price;
        }

        foreach (var item in basket.Items)
        {
            var productItem = await _productRepository.GetByIdAsync(item.Id, cancellationToken);
            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }

        var service = new PaymentIntentService();

        PaymentIntent intent = null;

        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)basket.Items.Sum(_ => _.Quantity * (_.Price * 100)) + (long)shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            intent = await service.CreateAsync(options, cancellationToken: cancellationToken);
            basket.PaymentIntentId = intent.Id;
            basket.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)basket.Items.Sum(_ => _.Quantity * (_.Price * 100)) + (long)shippingPrice * 100,
            };
            await service.UpdateAsync(basket.PaymentIntentId, options, cancellationToken: cancellationToken);
        }

        await _basketRepository.CreateOrUpdateBasketAsync(basket);

        return basket;
    }
}
