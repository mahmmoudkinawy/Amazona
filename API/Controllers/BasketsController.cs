namespace API.Controllers;

[Route("api/baskets")]
[ApiController]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;

    public BasketsController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    //All params must be as route to be requierd, but just I'm trying to test someting
    //And will fix all of this later
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBasketById([FromRoute] string id)
    {
        var basket = await _basketRepository.GetBasketByIdAsync(id);

        return Ok(basket ?? new CustomerBasketEntity(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateBasket([FromBody] CustomerBasketEntity basket)
    {
        var basketToCreateOrUpdate = await _basketRepository.CreateOrUpdateBasketAsync(basket);

        return Ok(basketToCreateOrUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBasket([FromRoute] string id)
    {
        if (await _basketRepository.DeleteBasketAsync(id))
        {
            return NoContent();
        }

        return BadRequest();
    }
}
