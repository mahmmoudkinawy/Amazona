namespace API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        return Ok(await _productRepository.GetProductsAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetProductBrands(CancellationToken cancellationToken)
    {
        return Ok(await _productRepository.GetProductBrandsAsync(cancellationToken));
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetProductTypes(CancellationToken cancellationToken)
    {
        return Ok(await _productRepository.GetProductTypesAsync(cancellationToken));
    }
}
