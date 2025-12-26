using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Net9WebApi.Wrappers;

namespace Net9WebApi.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var response = ApiResponse<object>.FailResponse(exception.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            // You can customize status code based on exception type here
            if (exception is ArgumentException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
