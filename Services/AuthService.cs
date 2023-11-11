using BoolBnB_MAUI.Data.Auth;
using BoolBnB_MAUI.Services.Interface;
using System.Text.Json;

namespace BoolBnB_MAUI.Services
{
    public class AuthService
    {
        private readonly IHttpService _httpService;
        private readonly JsonSerializerOptions _serializerOptions;
        private const string AuthKey = "AuthKey";

        public AuthService()
        {
            _httpService = new HttpService();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public static async Task<bool> IsAuthenticatedAsync()
        {
            string isAuth = await SecureStorage.GetAsync(AuthKey);
            Console.WriteLine($"TOKEN: {isAuth}");
            if (isAuth != null)
            {
                return true;
            }
            return false;
        }

        public static async Task<string> GetToken()
        {
            string isAuth = await SecureStorage.GetAsync(AuthKey);
            if (isAuth != null)
            {
                return isAuth;
            }
            return null;
        }

        public async Task<bool> Login(string email, string password)
        {
            var loginData = new { email , password };
            //Console.WriteLine("ContenT=> {0}",loginData);
            try
            {
                using HttpResponseMessage response = await _httpService.Post("/api/login", loginData);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<LoginResponse>(responseContent, _serializerOptions);

                    if (data.Success == true)
                    {
                        await SecureStorage.SetAsync(AuthKey, data.Token);
                        return true;
                    }
                }
                return false;
            }
            catch (HttpRequestException ex) 
            {
                Console.WriteLine($"Error Login API : {ex.Message}");
                return false;
            }            
        }

        public async Task<bool> Logout(string token)
        {
            try
            {
                using HttpResponseMessage response = await _httpService.Delete("/api/logout",token);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<LogoutResponse>(responseContent, _serializerOptions);

                    if (data.Success == true)
                    {
                        SecureStorage.Remove(AuthKey);
                        return true;
                    }
                }
                return false;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error Login API : {ex.Message}");
                return false;
            }
            
        }
    }
}
