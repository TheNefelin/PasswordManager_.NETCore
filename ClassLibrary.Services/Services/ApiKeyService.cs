using ClassLibrary.Services.Interfaces;
using Dapper;
using System.Data;

namespace ClassLibrary.Services.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IDbConnection _connection;

        public ApiKeyService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> ValidateApiKey(string apiKey)
        {
            try
            {
                var sqlApiKey = await _connection.QueryFirstOrDefaultAsync<string>(
                    $"SELECT ApiKey FROM Mae_Config WHERE Id = @Id",
                    new { Id = 1 });

                if (sqlApiKey == null) return false;

                return apiKey.Equals(sqlApiKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Error validating API Key.", ex);
            }
        }
    }
}
