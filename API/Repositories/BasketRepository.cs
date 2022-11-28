namespace API.Repositories;
public sealed class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<CustomerBasketEntity> CreateOrUpdateBasketAsync(CustomerBasketEntity basket)
    {
        var created = await _database.StringSetAsync(
            basket.Id,
            JsonSerializer.Serialize(basket),
            TimeSpan.FromDays(7));

        if (!created)
        {
            return null;
        }

        return await GetBasketByIdAsync(basket.Id);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await _database.KeyDeleteAsync(basketId);
    }

    public async Task<CustomerBasketEntity> GetBasketByIdAsync(string basketId)
    {
        var customerBasket = await _database.StringGetAsync(basketId);
        return customerBasket.IsNullOrEmpty
            ?
            null 
            : 
            JsonSerializer.Deserialize<CustomerBasketEntity>(customerBasket);
    }
}
