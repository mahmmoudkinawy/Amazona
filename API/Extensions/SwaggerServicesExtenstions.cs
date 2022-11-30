namespace API.Extensions;
public static class SwaggerServicesExtenstions
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(setupAction =>
        {
            setupAction.AddSecurityDefinition("AmazonaApiBearerAuth", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input a valid token to access this API"
            });

            setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "AmazonaApiBearerAuth" }
                    }, new List<string>() }
            });

        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI();

        return app;
    }
}
