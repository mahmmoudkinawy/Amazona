namespace API.Interfaces;
public interface IPaymentService
{
    Task<CustomerBasketEntity> CreateOrUpdatePaymentIntent(string basketId, CancellationToken cancellationToken);
}
