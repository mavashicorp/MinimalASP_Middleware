namespace MinimalASP.Middleware
{
    public class FooterValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HeaderValidationMiddleware> _logger;

        public FooterValidationMiddleware(RequestDelegate next, ILogger<HeaderValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-Special-Footer"))
            {
                _logger.LogWarning($"Access denied: Missing X-Special-Footer. Request for {context.Request.Path} was denied.");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            _logger.LogInformation("Access granted: X-Special-Header found.");
            await _next(context);
        }
    }

    public static class FooterValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FooterValidationMiddleware>();
        }
    }
}

