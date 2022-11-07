namespace API.Interfaces;
public interface IProductRepository
{
    Task<IReadOnlyList<ProductEntity>> GetProductsAsync(CancellationToken cancellationToken);
    Task<ProductEntity> GetProductByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductTypeEntity>> GetProductTypesAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductBrandEntity>> GetProductBrandsAsync(CancellationToken cancellationToken);
}
