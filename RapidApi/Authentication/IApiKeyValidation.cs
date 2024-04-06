namespace RapidApi.Authentication
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}
