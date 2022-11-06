namespace API.DbContexts;
public class AmazonaDbContext : DbContext
{
    public AmazonaDbContext(DbContextOptions<AmazonaDbContext> options) : base(options)
    { }

    public DbSet<ProductEntity> Products { get; set; }
}
