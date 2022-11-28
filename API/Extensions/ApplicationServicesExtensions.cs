namespace API.Extensions;
public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IBasketRepository, BasketRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddDbContext<AmazonaDbContext>(
            _ => _.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var configurations = ConfigurationOptions.Parse(
                config.GetConnectionString("Redis"),
                false);

            return ConnectionMultiplexer.Connect(configurations);
        });

        return services;
    }
}
