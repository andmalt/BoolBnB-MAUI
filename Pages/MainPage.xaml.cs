
namespace BoolBnB_MAUI.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ToggleOptions(object sender, EventArgs e)
        {
            // Show or hide the drop-down menu when the button is pressed
            optionsStack.IsVisible = !optionsStack.IsVisible;
        }

        private void GoToDashboard(object sender, EventArgs e)
        {
            // Action to go to dashboard
            Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }

        private void Logout(object sender, EventArgs e)
        {
            // Action to go to logout
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}