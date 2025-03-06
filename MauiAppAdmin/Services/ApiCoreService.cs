using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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

        public async Task<ResponseApi<CoreIVDTO>> GetIVAsync(CoreRequest<object> coreRequest, string apiKeyToken)
        {
            return await ApiRequest<CoreIVDTO, object>("/api/core/get-iv", coreRequest, apiKeyToken);
        }

        public async Task<ResponseApi<IEnumerable<CoreDTO>>> GetAllAsync(CoreRequest<object> coreRequest, string apiKeyToken)
        {
            return await ApiRequest<IEnumerable<CoreDTO>, object>("/api/core/get-all", coreRequest, apiKeyToken);
        }

        private async Task<ResponseApi<T>> ApiRequest<T, U>(string uri, CoreRequest<U> coreRequest, string apiKeyToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKeyToken);

                var json = JsonConvert.SerializeObject(coreRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PatchAsync(uri, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var responseApi = JsonConvert.DeserializeObject<ResponseApi<T>>(responseContent);

                if (responseApi.StatusCode == 0)
                {
                    return new ResponseApi<T>
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
                return new ResponseApi<T>()
                {
                    IsSucces = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }

    }
}
