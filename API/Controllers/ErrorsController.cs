namespace API.Controllers;

[ApiController]
[Route("api/errors")]
public sealed class ErrorsController : ControllerBase
{
    private readonly AmazonaDbContext _context;

    public ErrorsController(AmazonaDbContext context)
    {
        _context = context;
    }

    [HttpGet("not-found")]
    public IActionResult GetNotFound()
    {
        var product = _context.Products.Find(-1);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("server-error")]
    public IActionResult GetServerError()
    {
        var product = _context.Products.Find(-1);

        return Ok(product.ToString());
    }

    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        return BadRequest();
    }

    [HttpGet("bad-request/{id}")]
    public IActionResult GetNotFoundResouceWithBadRequest(int id)
    {
        return Ok();
    }
}
