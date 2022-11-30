var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddConfigureCors();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwaggerDocumentation();

//app.UseHttpsRedirection();

app.UseCors(Constants.CorsPolicyName);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
try
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AmazonaDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
    await dbContext.Database.MigrateAsync();
    await Seed.SeedData(dbContext);
    await Seed.SeedUsers(userManager);
}
catch (Exception ex)
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured while applying migrations");
}
await app.RunAsync();
