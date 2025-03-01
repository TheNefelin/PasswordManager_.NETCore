namespace ClassLibrary.Services.Interfaces
{
    public interface IApiKeyService
    {
        Task<bool> ValidateApiKey(string apiKey);
    }
}
