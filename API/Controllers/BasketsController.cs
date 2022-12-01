namespace API.Controllers;

[Route("api/baskets")]
[ApiController]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public BasketsController(
        IBasketRepository basketRepository,
        IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
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
    public async Task<IActionResult> CreateOrUpdateBasket([FromBody] CustomerBasketDto basket)
    {
        var basketToBeMapped = _mapper.Map<CustomerBasketEntity>(basket);

        var basketToCreateOrUpdate = await _basketRepository.CreateOrUpdateBasketAsync(basketToBeMapped);

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
