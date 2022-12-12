namespace API.Extensions;
public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IPaymentService, PaymentService>();

        services.AddScoped<IBasketRepository, BasketRepository>();

        services.AddScoped<ITokenService, Services.TokenService>();

        services.AddScoped<IOrderService, OrderService>();

        services.AddSingleton<IResponseCacheService, ResponseCacheService>();

        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var configurations = ConfigurationOptions.Parse(
                config.GetConnectionString("Redis"),
                false);

            return ConnectionMultiplexer.Connect(configurations);
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddDbContext<AmazonaDbContext>(
            _ => _.UseNpgsql(config.GetConnectionString("DefaultConnection")));


        return services;
    }
}
