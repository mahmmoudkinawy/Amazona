namespace API.Extensions;
public static class CorsExtensions
{
    public static IServiceCollection AddConfigureCors(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        var allowedOrigins = configuration.GetSection(Constants.CorsOriginSectionKey)
            .GetChildren()
            .Select(_ => _.Value)
            .ToArray();

        if (!allowedOrigins.Any()) return services;

        services.AddCors(_ =>
        {
            _.AddPolicy(Constants.CorsPolicyName, _ =>
            {
                _
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(allowedOrigins);
            });
        });

        return services;
    }
}
