
using BoolBnB_MAUI.Services;

namespace BoolBnB_MAUI.Pages
{
    public partial class MainPage : ContentPage
    {
        private bool IsAuth {  get; set; }
        private readonly AuthService _authService;
        public MainPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            optionsStackNoAuth.IsVisible = false;
            optionsStackAuth.IsVisible = false;

            if (await _authService.IsAuthenticatedAsync())
            {
                IsAuth = true;
            }
            else
            {
                IsAuth = false;
            }
        }

        private void ToggleOptions(object sender, EventArgs e)
        {
            if (IsAuth)
            {
                // Show or hide the drop-down menu when the button is pressed
                optionsStackAuth.IsVisible = !optionsStackAuth.IsVisible;          
            }
            else
            {
                // Show or hide the drop-down menu when the button is pressed
                optionsStackNoAuth.IsVisible = !optionsStackNoAuth.IsVisible;
            }           
        }

        private void GoToDashboard(object sender, EventArgs e)
        {
            // Action to go to dashboard
            Shell.Current.GoToAsync($"//{nameof(InfoPage)}");
        }

        private async void Logout(object sender, EventArgs e)
        {
            var token = await _authService.GetToken();
            await _authService.Logout(token);
            // Action to go to logout
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        
        private void Login(object sender, EventArgs e)
        {
            // Action to go to login
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private void GoToHomesPages(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(HomesPage)}");
        }
    }
}