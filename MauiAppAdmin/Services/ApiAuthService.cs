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

        public async Task<ResponseApi<UserIdDTO>> RegisterAsync(AuthRegister authRegister)
        {
            return await ApiRequest<UserIdDTO, AuthRegister>(authRegister, "/api/auth/register");
        }

        public async Task<ResponseApi<AuthLogged>> LoginAsync(AuthLogin authLogin)
        {
            return await ApiRequest<AuthLogged, AuthLogin>(authLogin, "/api/auth/login");
        }

        private async Task<ResponseApi<T>> ApiRequest<T,U>(U obj, string uri)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(uri, content);
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
