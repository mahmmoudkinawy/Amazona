namespace API.Configurations;
public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.OwnsOne(_ => _.ItemOrdered, _ => _.WithOwner());
    }
}
