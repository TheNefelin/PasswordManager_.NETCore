using ClassLibrary.Models.DTOs;

namespace MauiAppAdmin.Services
{
    public class Storage
    {
        public async static Task SaveUser(AuthLogged authLogged)
        {
            await SecureStorage.SetAsync("idUser", authLogged.IdUser);
            await SecureStorage.SetAsync("apiToken", authLogged.ApiToken);
            await SecureStorage.SetAsync("sqlToken", authLogged.SqlToken);
            await SecureStorage.SetAsync("role", authLogged.Role);
            await SecureStorage.SetAsync("expireMin", authLogged.ExpireMin);;
            await SecureStorage.SetAsync("loginDate", DateTime.Now.ToString("O"));
        }

        public async static Task<AuthLogged> GetUser()
        {
            return new AuthLogged
            {
                IdUser = await SecureStorage.GetAsync("idUser") ?? string.Empty,
                ApiToken = await SecureStorage.GetAsync("apiToken") ?? string.Empty,
                SqlToken = await SecureStorage.GetAsync("sqlToken") ?? string.Empty,
                Role = await SecureStorage.GetAsync("role") ?? string.Empty,
                ExpireMin = await SecureStorage.GetAsync("expireMin") ?? string.Empty,
            };
        }

        public async static Task<string> GetLoginDate()
        {
            return await SecureStorage.GetAsync("loginDate") ?? string.Empty;
        }

        public async static Task RemoveUser()
        {
            //SecureStorage.RemoveAll();
            SecureStorage.Remove("idUser");
            SecureStorage.Remove("apiToken");
            SecureStorage.Remove("sqlToken");
            SecureStorage.Remove("role");
            SecureStorage.Remove("expireMin");
            SecureStorage.Remove("loginDate");
        }
    }
}
