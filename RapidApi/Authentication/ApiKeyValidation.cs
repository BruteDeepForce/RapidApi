using RapidApi.Model;
using System.Runtime.CompilerServices;

namespace RapidApi.Authentication
{
    public class ApiKeyValidation : IApiKeyValidation
    {
        private readonly IConfiguration _configuration;
        private readonly Context context;

        public ApiKeyValidation(IConfiguration configuration, Context _c)
        {
            _configuration = configuration;
            context = _c;
        }
        public bool IsValidApiKey(string userApiKey)
        {
            if(string.IsNullOrWhiteSpace(userApiKey))
            {
                return false;
            }

            //string? apikey = _configuration.GetValue<string>(AuthConstants.ApiKeyName);
            
            foreach (var key in context.apiKeys)
            {
                if(key.ApiKey==userApiKey)
                    return true;


                //if (key == null || key != userApiKey)
                //    return false;
            }
            return false;
            
        }
    }
}
