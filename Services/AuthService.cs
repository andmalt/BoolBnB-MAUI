using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoolBnB_MAUI.Services
{
    public class AuthService
    {
        private const string AuthKey = "AuthKey";

        public async Task<bool> IsAuthenticatedAsync()
        {
            string isAuth = await SecureStorage.GetAsync(AuthKey);
            if (isAuth != null)
            {
                return true;
            }
            return false;
        }

        public async Task Login()
        {
            await SecureStorage.SetAsync(AuthKey, "akHJLMXkajs3242dkajs");
        }

        public void Logout()
        {
            SecureStorage.Remove(AuthKey);
        }
    }
}
