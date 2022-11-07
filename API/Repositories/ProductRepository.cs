namespace API.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly AmazonaDbContext _context;

    public ProductRepository(AmazonaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductBrandEntity>> GetProductBrandsAsync(
        CancellationToken cancellationToken)
    {
        return await _context
            .ProductBrands
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductEntity> GetProductByIdAsync(
        int id, CancellationToken cancellationToken)
    {
        return await _context
            .Products
            .Include(_ => _.ProductBrand)
            .Include(_ => _.ProductType)
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<ProductEntity>> GetProductsAsync(
        CancellationToken cancellationToken)
    {
        return await _context
            .Products
            .Include(_ => _.ProductBrand)
            .Include(_ => _.ProductType)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductTypeEntity>> GetProductTypesAsync(
        CancellationToken cancellationToken)
    {
        return await _context
            .ProductTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
