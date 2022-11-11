namespace API.Specifications;
public class ProductsWithFilterForCountSpecification : BaseSpecification<ProductEntity>
{
    public ProductsWithFilterForCountSpecification(ProductSpecParams productParams)
        : base
            (
                _ =>
                    (string.IsNullOrEmpty(productParams.Search) || _.Name.ToLower().Contains(productParams.Search)) &&
                    (!productParams.BrandId.HasValue || _.ProductBrandId == productParams.BrandId) &&
                    (!productParams.TypeId.HasValue || _.ProductTypeId == productParams.TypeId)
            )
    { }

}
