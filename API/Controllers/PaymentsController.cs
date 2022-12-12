namespace API.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<IActionResult> CreateOrUpdatePaymentIntent(
        [FromRoute] string basketId, CancellationToken cancellationToken)
    {
        return Ok(await _paymentService.CreateOrUpdatePaymentIntent(basketId, cancellationToken));
    }
}
