namespace API.Helpers;
public sealed class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProductEntity, ProductToReturnDto>()
            .ForMember(_ => _.ProductType, _ => _.MapFrom(_ => _.ProductType.Name))
            .ForMember(_ => _.ProductBrand, _ => _.MapFrom(_ => _.ProductBrand.Name));

        CreateMap<AddressEntity, AddressDto>().ReverseMap();

        CreateMap<BasketItemDto, BasketItemEntity>();

        CreateMap<CustomerBasketDto, CustomerBasketEntity>();

        CreateMap<AddressDto, AddressOwned>();

        CreateMap<OrderEntity, OrderToReturnDto>()
            .ForMember(_ => _.DeliveryMethod, _ => _.MapFrom(_ => _.DeliveryMethod.ShortName))
            .ForMember(_ => _.ShippingPrice, _ => _.MapFrom(_ => _.DeliveryMethod.Price));

        CreateMap<OrderItemEntity, OrderItemDto>()
            .ForMember(_ => _.ProductItemId, _ => _.MapFrom(_ => _.ItemOrdered.ProductItemId))
            .ForMember(_ => _.ProductName, _ => _.MapFrom(_ => _.ItemOrdered.ProductName))
            .ForMember(_ => _.PictureUrl, _ => _.MapFrom(_ => _.ItemOrdered.PictureUrl));
    }
}
