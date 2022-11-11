namespace API.Specifications;
public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<ProductEntity>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
    {
        AddInclude(_ => _.ProductType);
        AddInclude(_ => _.ProductBrand);
        AddOrderBy(_ => _.Name);

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(_ => _.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(_ => _.Price);
                    break;
                default:
                    AddOrderBy(_ => _.Name);
                    break;
            }
        }
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(_ => _.Id == id)
    {
        AddInclude(_ => _.ProductType);
        AddInclude(_ => _.ProductBrand);
    }
}
