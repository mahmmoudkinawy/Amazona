namespace API.Specifications;
public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<ProductEntity>
{
    public ProductsWithTypesAndBrandsSpecification()
    {
        AddInclude(_ => _.ProductType);
        AddInclude(_ => _.ProductBrand);
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(_ => _.Id == id)
    {
        AddInclude(_ => _.ProductType);
        AddInclude(_ => _.ProductBrand);
    }
}
