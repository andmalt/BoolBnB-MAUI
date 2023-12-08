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

        public async Task<bool> IsAuthenticatedAsync()
        {
            string isAuth = await SecureStorage.GetAsync(AuthKey);
            Console.WriteLine($"TOKEN: {isAuth}");
            if (isAuth != null)
            {
                return true;
            }
            return false;
        }

        public async Task<string> GetToken()
        {
            string isAuth = await SecureStorage.GetAsync(AuthKey);
            if (isAuth != null)
            {
                return isAuth;
            }
            return null;
        }

        public bool DestroyToken()
        {
            return SecureStorage.Remove(AuthKey);
        }

        public async Task<bool> Login(string email, string password)
        {
            var loginData = new LoginRequest { email = email, password = password };
            //Console.WriteLine("ContenT=> {0}",loginData);
            try
            {
                var response = await _httpService.Post<LoginResponse,LoginRequest>("/api/login", loginData);
                if (response.Success)
                { 
                        await SecureStorage.SetAsync(AuthKey, response.Token);
                        return true;
                }
                return false;
            }
            catch (HttpRequestException ex) 
            {
                Console.WriteLine($"Error Login API : {ex.Message}");
                throw;
            }            
        }

        public async Task<bool> Logout(string token)
        {
            try
            {
                var response = await _httpService.Delete<LogoutResponse>("/api/logout", token);
                if (response.Success)
                {
                    SecureStorage.Remove(AuthKey);
                    return true;
                }
                SecureStorage.Remove(AuthKey);
                return false;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error Login API : {ex.Message}");
                // add a modal to show network error
                throw;
            }

        }
    }
}
