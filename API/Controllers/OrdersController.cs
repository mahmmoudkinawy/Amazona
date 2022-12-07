namespace API.Controllers;

[Route("api/orders")]
[ApiController]
[Authorize]
public sealed class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        [FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        var email = User.RetrieveEmailFromPrinciple();

        var address = _mapper.Map<AddressOwned>(orderDto.ShipToAddress);

        var order = await _orderService.CreateOrderAsync(
            email,
            orderDto.DeliveryMethodId,
            orderDto.BasketId,
            address,
            cancellationToken);

        if (order is null)
        {
            return BadRequest("Problem creating order");
        }

        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderForUser(CancellationToken cancellationToken)
    {
        var email = User.RetrieveEmailFromPrinciple();

        var orders = await _orderService.GetOrdersForUserAsync(email, cancellationToken);

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderForUserById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var email = User.RetrieveEmailFromPrinciple();

        var order = await _orderService.GetOrderByIdAsync(id, email, cancellationToken);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpGet("delivery-methods")]
    public async Task<IActionResult> GetDeliveryMethods(CancellationToken cancellationToken)
    {
        return Ok(await _orderService.GetDeliveryMethodsAsync(cancellationToken));
    }
}
