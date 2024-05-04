﻿namespace MinimalASP.Middleware
{
    public class HeaderValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HeaderValidationMiddleware> _logger;

        public HeaderValidationMiddleware(RequestDelegate next, ILogger<HeaderValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-Special-Header"))
            {
                _logger.LogWarning($"Access denied: Missing X-Special-Header. Request for {context.Request.Path} was denied.");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            _logger.LogInformation("Access granted: X-Special-Header found.");
            await _next(context);
        }
    }

    public static class HeaderValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderValidationMiddleware>();
        }
    }
}

