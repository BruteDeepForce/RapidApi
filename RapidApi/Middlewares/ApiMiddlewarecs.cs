using System.Collections;

namespace RapidApi.Middlewares
{
    public class UseApiMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UseApiMiddleWare> _logger;
        public UseApiMiddleWare(RequestDelegate next, ILogger<UseApiMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var pathClick = context.Request.Path;
            var startTime = DateTime.UtcNow;

            var req = context.Request;       

            await _next.Invoke(context);

            var endTime = DateTime.UtcNow;

            var sum = endTime-startTime;

            _logger.LogInformation($"{pathClick} Endpointinde Request - Response arası Süre : {sum.TotalMilliseconds} MiliSecond");

            _logger.LogInformation($"{pathClick}");
        }
    }
    public static class ApiMiddlewareExtension
    {
        public static IApplicationBuilder UseApiMiddleware(this IApplicationBuilder builder) 
        {
            return builder.UseMiddleware<UseApiMiddleWare>();
        }

        public static IApplicationBuilder UseCheckRemoteMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckRemoteIpAdressMiddleware>();
        }

    }
}
