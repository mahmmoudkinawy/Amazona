namespace API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IRepository<ProductEntity> _productsRepository;
    private readonly IRepository<ProductBrandEntity> _productBrandsRepository;
    private readonly IRepository<ProductTypeEntity> _productTypesRepository;

    public ProductsController(
        IRepository<ProductEntity> productsRepository,
        IRepository<ProductBrandEntity> productBrandsRepository,
        IRepository<ProductTypeEntity> productTypesRepository)
    {
        _productsRepository = productsRepository;
        _productBrandsRepository = productBrandsRepository;
        _productTypesRepository = productTypesRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await _productsRepository.GetEntitiesWithSpecAsync(spec, cancellationToken);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productsRepository.GetEntityWithSpecAsync(spec, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetProductBrands(CancellationToken cancellationToken)
    {
        return Ok(await _productBrandsRepository.GetAllAsync(cancellationToken));
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetProductTypes(CancellationToken cancellationToken)
    {
        return Ok(await _productTypesRepository.GetAllAsync(cancellationToken));
    }
}
