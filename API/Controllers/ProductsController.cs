using API.Helpers;
using System.Text.Json.Serialization;

namespace API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IRepository<ProductEntity> _productsRepository;
    private readonly IRepository<ProductBrandEntity> _productBrandsRepository;
    private readonly IRepository<ProductTypeEntity> _productTypesRepository;
    private readonly IMapper _mapper;

    public ProductsController(
        IRepository<ProductEntity> productsRepository,
        IRepository<ProductBrandEntity> productBrandsRepository,
        IRepository<ProductTypeEntity> productTypesRepository,
        IMapper mapper)
    {
        _productsRepository = productsRepository;
        _productBrandsRepository = productBrandsRepository;
        _productTypesRepository = productTypesRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] ProductSpecParams productParams, 
        CancellationToken cancellationToken)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

        var products = await _productsRepository
            .GetEntitiesWithSpecAsync(spec, cancellationToken);

        var countSpec = new ProductsWithFilterForCountSpecification(productParams);

        var totalItems = await _productsRepository
            .GetCountWithSpecAsync(countSpec, cancellationToken);

        var productsToReturn = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

        return Ok
            (
                new Pagination<ProductToReturnDto>(
                    productParams.PageSize,
                    productParams.PageIndex,
                    totalItems,
                    productsToReturn)
            );
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

        return Ok(_mapper.Map<ProductToReturnDto>(product));
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
