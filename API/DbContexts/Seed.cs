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

    public static async Task SeedUsers(UserManager<UserEntity> userManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var user = new UserEntity
        {
            DisplayName = "Bob",
            Email = "bob@test.com",
            UserName = "bob@test.com",
            Address = new AddressEntity
            {
                FirstName = "bob",
                LastName = "bob",
                City = "Shebin-Elkom",
                State = "Menofia",
                Street = "Salah-Eldeen",
                ZipCode = "01203bpA"
            }
        };

        await userManager.CreateAsync(user, "Pa$$w0rd");
    }

    public static async Task SeedDeliveryMethods(AmazonaDbContext context)
    {
        if (!await context.DeliveryMethods.AnyAsync()) return;

        var deliveryMethods = new List<DeliveryMethodEntity>
        {
            new DeliveryMethodEntity
            {
                ShortName = "UPS1",
                Description = "Fastest delivery time",
                DeliveryTime = "1-2 Days",
                Price = 10
            },
            new DeliveryMethodEntity
            {
                ShortName = "UPS2",
                Description = "Get it within 5 days",
                DeliveryTime = "2-5 Days",
                Price = 5
            },
            new DeliveryMethodEntity
            {
                ShortName = "UPS3",
                Description = "Slower but cheap",
                DeliveryTime = "5-10 Days",
                Price = 2
            },
            new DeliveryMethodEntity
            {
                ShortName = "FREE",
                Description = "Free! You get what you pay for",
                DeliveryTime = "1-2 Weeks",
                Price = 0
            }
        };

        context.DeliveryMethods.AddRange(deliveryMethods);
        await context.SaveChangesAsync();
    }
}
