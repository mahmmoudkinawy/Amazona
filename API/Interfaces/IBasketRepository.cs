namespace API.Interfaces;
public interface IBasketRepository
{
    Task<CustomerBasketEntity> GetBasketByIdAsync(string basketId);
    Task<CustomerBasketEntity> CreateOrUpdateBasketAsync(CustomerBasketEntity basket);
    Task<bool> DeleteBasketAsync(string basketId);
}
