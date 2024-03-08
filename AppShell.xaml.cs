using BoolBnB_MAUI.Pages.Main;
using BoolBnB_MAUI.Services;

namespace BoolBnB_MAUI
{
    public partial class AppShell : Shell
    {
        protected readonly AuthService _authService;
        public AppShell()
        {
            InitializeComponent();
            _authService = new AuthService();
            App.Current.UserAppTheme = AppTheme.Light;
        }

        private async void Logout(object sender, EventArgs e)
        {
            var token = await _authService.GetToken();
            var isLogout = await _authService.Logout(token);
            if (isLogout)
            {
                //
                await Current.GoToAsync($"//{nameof(MainPage)}");
            }
            else
            {
                //
            }
        }
    }
}