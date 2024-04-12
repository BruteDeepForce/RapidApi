using Microsoft.AspNetCore.Mvc;

namespace RapidApi.Model
{
    public class TimeInsertDB : ITimeInsertDatabase
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public TimeInsertDB(Context context, IHttpContextAccessor contextAccessor) { _context = context; _contextAccessor = contextAccessor; }

        public void TimeInsert(RequestModel model)
        {

                var httpContext = _contextAccessor.HttpContext;
                //var httpContext = new DefaultHttpContext();
                var apikeyEntity = _context.apiKeys.FirstOrDefault(x => x.ApiKey == model.ApiKey);

                apikeyEntity.ipAdress = httpContext.Connection.RemoteIpAddress.ToString();

                apikeyEntity.RequestTime = DateTime.Now;

                _context.SaveChanges();
        }
    }
}
