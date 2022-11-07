namespace Amazona.Tests.Repositories;
public class ProductRepositoryFake : IProductRepository
{
    private readonly List<ProductEntity> _products;
    private readonly List<ProductBrandEntity> _productBrands;
    private readonly List<ProductTypeEntity> _productTypes;

    public ProductRepositoryFake()
    {
        _products = new List<ProductEntity>
        {
            new ProductEntity
            {
                Id = 1,
                Description = "lorem inpusm",
                Name = "product 1",
                PictureUrl = "http://placeholder/500*500",
                ProductBrandId = 1,
                ProductTypeId = 2,
                Price = 15
            },
            new ProductEntity
            {
                Id = 2,
                Description = "lorem inpusm",
                Name = "product 2",
                PictureUrl = "http://placeholder/500*500",
                ProductBrandId = 2,
                ProductTypeId = 3,
                Price = 25
            },
            new ProductEntity
            {
                Id = 3,
                Description = "lorem inpusm",
                Name = "product 3",
                PictureUrl = "http://placeholder/500*500",
                ProductBrandId = 3,
                ProductTypeId = 3,
                Price = 29
            },
        };

        _productBrands = new List<ProductBrandEntity>
        {
            new ProductBrandEntity
            {
               Id = 1,
               Name = "test 1"
            },
            new ProductBrandEntity
            {
               Id = 2,
               Name = "test 2"
            },
            new ProductBrandEntity
            {
               Id = 3,
               Name = "test 3"
            },
        };

        _productTypes = new List<ProductTypeEntity>
        {
            new ProductTypeEntity
            {
               Id = 1,
               Name = "test 1"
            },
            new ProductTypeEntity
            {
               Id = 2,
               Name = "test 2"
            },
            new ProductTypeEntity
            {
               Id = 3,
               Name = "test 3"
            }
        };
    }

    public async Task<IReadOnlyList<ProductBrandEntity>> GetProductBrandsAsync(
        CancellationToken cancellationToken)
    {
        return _productBrands.ToList();
    }

    public async Task<ProductEntity> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _products.FirstOrDefault(_ => _.Id == id);
    }

    public async Task<IReadOnlyList<ProductEntity>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return _products.ToList();
    }

    public async Task<IReadOnlyList<ProductTypeEntity>> GetProductTypesAsync(CancellationToken cancellationToken)
    {
        return _productTypes.ToList();
    }
}
