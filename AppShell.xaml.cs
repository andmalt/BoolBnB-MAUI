using BoolBnB_MAUI.Pages;

namespace BoolBnB_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}