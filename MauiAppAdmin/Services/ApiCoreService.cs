using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MauiAppAdmin.Services
{
    public class ApiCoreService : IApiCoreService
    {
        private readonly HttpClient _httpClient;

        public ApiCoreService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authLogged.ApiToken);
            if (!_httpClient.DefaultRequestHeaders.Contains("ApiKey"))
            {
                _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
            }
        }

        public async Task<ResponseApi<IEnumerable<CoreDTO>>> GetAllAsync(CoreRequest<object> coreRequest, string apiKeyToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKeyToken);

                var json = JsonConvert.SerializeObject(coreRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PatchAsync("/api/core/get-all", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var responseApi = JsonConvert.DeserializeObject<ResponseApi<IEnumerable<CoreDTO>>>(responseContent);

                if (responseApi.StatusCode == 0)
                {
                    return new ResponseApi<IEnumerable<CoreDTO>>
                    {
                        IsSucces = false,
                        StatusCode = (int)response.StatusCode,
                        Message = responseContent,
                    };
                }

                return responseApi;
            }
            catch (Exception ex)
            {
                return new ResponseApi<IEnumerable<CoreDTO>>()
                {
                    IsSucces = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }

    }
}
