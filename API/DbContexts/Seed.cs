namespace API.DbContexts;
public static class Seed
{
    public static async Task SeedData(AmazonaDbContext context)
    {
        if (await context.Products.AnyAsync()) return;

        var brandFaker = new Faker<ProductBrandEntity>()
            .RuleFor(_ => _.Name, _ => _.Commerce.Categories(3).FirstOrDefault());

        foreach (var brand in brandFaker.Generate(3))
        {
            context.ProductBrands.Add(brand);
        }
        await context.SaveChangesAsync();

        var typeFaker = new Faker<ProductTypeEntity>()
            .RuleFor(_ => _.Name, _ => _.Commerce.Department());

        foreach (var type in typeFaker.Generate(6))
        {
            context.ProductTypes.Add(type);
        }
        await context.SaveChangesAsync();

        var productFaker = new Faker<ProductEntity>()
            .RuleFor(_ => _.Name, _ => _.Commerce.Product())
            .RuleFor(_ => _.Description, _ => _.Commerce.ProductDescription())
            .RuleFor(_ => _.Price, _ => Convert.ToDecimal(_.Commerce.Price(5, 18)))
            .RuleFor(_ => _.PictureUrl, _ => _.Image.PicsumUrl())
            .RuleFor(_ => _.ProductTypeId, _ => new Random().Next(1, 6))
            .RuleFor(_ => _.ProductBrandId, _ => new Random().Next(1, 3));

        foreach (var product in productFaker.Generate(15))
        {
            context.Products.Add(product);
        }

        await context.SaveChangesAsync();
    }
}
