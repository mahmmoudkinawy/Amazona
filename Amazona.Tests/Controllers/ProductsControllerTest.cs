namespace Amazona.Tests.Controllers;
public class ProductsControllerTest
{
    private readonly ProductsController _controller;
    private readonly IProductRepository _repository;
    public ProductsControllerTest()
    {
        _repository = new ProductRepositoryFake();
        _controller = new ProductsController(_repository);
    }

    [Fact]
    public async Task GetProductBrands_WhenCalled_ReturnsOkResult()
    {
        var okResult = await _controller.GetProductBrands(new CancellationToken());

        Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
    }

    [Fact]
    public async Task GetProductTypes_WhenCalled_ReturnsOkResult()
    {
        var okResult = await _controller.GetProductTypes(new CancellationToken());

        Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
    }

    [Fact]
    public async Task GetProducts_WhenCalled_ReturnsOkResult()
    {
        var okResult = await _controller.GetProducts(new CancellationToken());

        Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
    }

    [Fact]
    public async Task GetProductById_WhenCalled_ReturnsOkResult()
    {
        var okResult = await _controller.GetProduct(1, new CancellationToken()) as OkObjectResult;

        Assert.IsType<OkObjectResult>(okResult);
        Assert.Equal(1, (okResult.Value as ProductEntity).Id);
    }

    [Fact]
    public async Task GetProductById_WhenCalled_ReturnsNotFoundResult()
    {
        var notFoundResult = await _controller.GetProduct(-1, new CancellationToken());

        Assert.IsType<NotFoundResult>(notFoundResult as NotFoundResult);
    }
}
