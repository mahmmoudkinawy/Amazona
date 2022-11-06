namespace API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AmazonaDbContext _context;

    public ProductsController(AmazonaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        return Ok(await _context.Products.ToListAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(
            new object?[] { id },
            cancellationToken: cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}
