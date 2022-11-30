namespace API.DbContexts;
public sealed class AmazonaDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int,
    IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public AmazonaDbContext(DbContextOptions<AmazonaDbContext> options) : base(options)
    { }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductTypeEntity> ProductTypes { get; set; }
    public DbSet<ProductBrandEntity> ProductBrands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
