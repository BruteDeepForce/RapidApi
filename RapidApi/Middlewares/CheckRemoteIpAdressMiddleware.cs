using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using RapidApi.Controllers;
using RapidApi.Model;
using System.Text;

namespace RapidApi.Middlewares
{
    public class CheckRemoteIpAdressMiddleware
    {
        private readonly RequestDelegate _next;
        
        public CheckRemoteIpAdressMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, Context dbContext) 
        {
            ApiKeys json = null;
            var startTime = DateTime.UtcNow;
            var ipAdress = context.Connection.RemoteIpAddress;

            var request = context.Request;
            if (request.ContentLength > 0)
            {
                request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                //get body string here...
                var requestContent = Encoding.UTF8.GetString(buffer);

                request.Body.Position = 0;

                json = JsonConvert.DeserializeObject<ApiKeys>(requestContent);

                var apiKeyRecord = dbContext.apiKeys.FirstOrDefault(x => x.ipAdress == ipAdress.ToString()
            && x.ApiKey == json.ApiKey);


                if (apiKeyRecord != null)
                {
                    var timeControl = startTime - apiKeyRecord.RequestTime;

                    if (timeControl.Value.TotalMinutes > 15)
                    {
                        await _next.Invoke(context);
                    }
                    throw new Exception("15 dakika geçmedi");

                }

            }
            
            await _next.Invoke(context);
        }

    }
}
