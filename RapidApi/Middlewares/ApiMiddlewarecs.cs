﻿namespace RapidApi.Middlewares
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
            var startTime = DateTime.UtcNow.Millisecond;

            var req = context.Request;

            //Console.WriteLine(startTime.ToString());

            await _next.Invoke(context);

            var endTime = DateTime.UtcNow.Millisecond;

            var sum = endTime-startTime;

            Console.WriteLine($"Request - Response arası Süre : {sum.ToString()} MiliSecond");
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