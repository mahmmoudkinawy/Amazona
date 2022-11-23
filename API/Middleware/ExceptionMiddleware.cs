namespace API.Middleware;
public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        IWebHostEnvironment env,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = _env.IsDevelopment() ?
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = ex.Message,
                    Detail = ex.StackTrace
                }
                :
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "Internal Server Error"
                };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
