﻿using BoolBnB_MAUI.Pages.CreateUpdateHouse;
using BoolBnB_MAUI.Pages.Home;
using BoolBnB_MAUI.Pages.Homes;
using BoolBnB_MAUI.Pages.Info;
using BoolBnB_MAUI.Pages.Login;
using BoolBnB_MAUI.Pages.MyHouses;
using BoolBnB_MAUI.Pages.MyHouse;
using BoolBnB_MAUI.Pages.Settings;
using BoolBnB_MAUI.Pages.Messages;
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

            Routing.RegisterRoute(nameof(HomesPage), typeof(HomesPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(CreateUpdateHousePage), typeof(CreateUpdateHousePage));
            Routing.RegisterRoute(nameof(InfoPage), typeof(InfoPage));
            Routing.RegisterRoute(nameof(MyHousesPage), typeof(MyHousesPage));
            Routing.RegisterRoute(nameof(MyHousePage), typeof(MyHousePage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(MessagesPage), typeof(MessagesPage));
        }

        private async void Logout(object sender, EventArgs e)
        {
            var token = await _authService.GetToken();
            var isLogout = await _authService.Logout(token);
            if (isLogout)
            {
                //
                await Current.GoToAsync($"//{nameof(HomesPage)}");
            }
            else
            {
                //
            }
        }
    }
}