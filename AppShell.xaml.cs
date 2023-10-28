using BoolBnB_MAUI.Pages;

namespace BoolBnB_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(HomesPage), typeof(HomesPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(CreateHousePage), typeof(CreateHousePage));
            Routing.RegisterRoute(nameof(UpdateHousePage), typeof(UpdateHousePage));
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(MyHousesPage), typeof(MyHousesPage));
            Routing.RegisterRoute(nameof(MyHousePage), typeof(MyHousePage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(MessagesPage), typeof(MessagesPage));
        }
    }
}