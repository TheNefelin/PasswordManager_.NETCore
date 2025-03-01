using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MauiAppAdmin.Services
{
    public class ApiAuthService : IApiAuthService
    {
        private readonly HttpClient _httpClient;

        public ApiAuthService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!_httpClient.DefaultRequestHeaders.Contains("ApiKey"))
            {
                _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
            }
        }

        public async Task<ResponseApi<AuthLogged>> LoginAsync(AuthLogin authLogin)
        {
            try
            {
                var json = JsonConvert.SerializeObject(authLogin);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/auth/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var responseApi = JsonConvert.DeserializeObject<ResponseApi<AuthLogged>>(responseContent);

                if (responseApi.StatusCode == 0)
                {
                    return new ResponseApi<AuthLogged>
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
                return new ResponseApi<AuthLogged>()
                {
                    IsSucces = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }
    }
}
