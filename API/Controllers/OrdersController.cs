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
}
