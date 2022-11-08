namespace API.Helpers;
public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<ProductEntity, ProductToReturnDto>()
			.ForMember(_ => _.ProductType, _ => _.MapFrom(_ => _.ProductType.Name))
			.ForMember(_ => _.ProductBrand, _ => _.MapFrom(_ => _.ProductBrand.Name));
    }
}
