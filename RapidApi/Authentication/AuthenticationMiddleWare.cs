//namespace RapidApi.Authentication
//{
//    public class AuthenticationMiddleWare
//    {
//        private readonly RequestDelegate _next;
//        private readonly IConfiguration _configuration;
//        public AuthenticationMiddleWare(RequestDelegate next)
//        {
//            _next = next;
//        }
//        public async Task InvokeAsync(HttpContext context)
//        {
//            if(!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync("Api Key Missing");
//                return;

//            }

//            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
//            if(!apiKey.Equals(extractedApiKey)) 
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync("Invalid Api Key");
//                return;           
//            }
//            await _next(context);
//        }

//    }
//}
