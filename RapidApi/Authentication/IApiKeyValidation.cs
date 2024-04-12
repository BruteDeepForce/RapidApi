namespace RapidApi.Authentication
{
    public interface IApiKeyValidation
    {
        bool IsExistApiKey(string userApiKey);
    }
}
