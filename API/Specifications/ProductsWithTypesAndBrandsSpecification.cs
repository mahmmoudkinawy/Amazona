namespace API.Specifications;
public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<ProductEntity>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
        : base
            (
                _ =>
                    (string.IsNullOrEmpty(productParams.Search) || _.Name.ToLower().Contains(productParams.Search)) &&
                    (!productParams.BrandId.HasValue || _.ProductBrandId == productParams.BrandId) &&
                    (!productParams.TypeId.HasValue || _.ProductTypeId == productParams.TypeId)
            )
    {
        AddInclude(_ => _.ProductType);
        AddInclude(_ => _.ProductBrand);
        AddOrderBy(_ => _.Name);
        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);


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
