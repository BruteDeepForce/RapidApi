namespace RapidApi.Authentication
{
    public class AuthConstants
    {
        public const string ApiKeyHeaderName = "X-API-Key";
        public const string ApiKeyName = "ApiKey";
    }

    public class RequestModel
    {
        public string? ApiKey { get; set; }

    }
}
