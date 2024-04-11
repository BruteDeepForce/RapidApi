namespace RapidApi.Middlewares
{
    public class UseApiMiddleWare
    {
        private readonly RequestDelegate _next;
        public UseApiMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var pathClick = context.Request.Path;

                var startTime = DateTime.UtcNow;

                var req = context.Request;

                await _next.Invoke(context);

                var endTime = DateTime.UtcNow;

                var sum = endTime - startTime;

            Console.WriteLine($"{pathClick} Endpoint Request - Response arası Süre : {sum.TotalMilliseconds}");
        }

    }

    public static class ApiMiddlewareExtension
    {
        public static IApplicationBuilder UseApiMiddleware(this IApplicationBuilder builder) 
        {
            return builder.UseMiddleware<UseApiMiddleWare>();
        }

    }
}
