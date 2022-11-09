namespace API.Extensions;
public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddDbContext<AmazonaDbContext>(
            _ => _.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        return services;
    }
}
