namespace API.Configurations;
public sealed class OrderCondifuration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.OwnsOne(_ => _.ShipToAddress, _ => _.WithOwner());

        builder.Property(_ => _.Status)
            .HasConversion(_ => _.ToString(),
                _ => (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), _));

        builder.HasMany(_ => _.OrderItems)
            .WithOne()
            .HasForeignKey(_ => _.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
