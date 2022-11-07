namespace API.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder
            .Property(_ => _.Name)
            .HasMaxLength(255)
            .IsRequired();
        builder
            .Property(_ => _.Description)
            .IsRequired();
        builder
            .Property(_ => _.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder
            .Property(_ => _.PictureUrl)
            .IsRequired();
        builder
            .HasOne(_ => _.ProductBrand)
            .WithMany()
            .HasForeignKey(_ => _.ProductBrandId);
        builder
            .HasOne(_ => _.ProductType)
            .WithMany()
            .HasForeignKey(_ => _.ProductTypeId);
    }
}
