using BoolBnB_MAUI.Pages;
using BoolBnB_MAUI.Services;

namespace BoolBnB_MAUI.Components;

public partial class HeaderContent : ContentView
{
    public static readonly BindableProperty IsAuthenticatedProperty =
            BindableProperty.Create(nameof(IsAuthenticated), typeof(bool), typeof(HeaderContent));

    public bool IsAuthenticated
    {
        get => (bool)GetValue(IsAuthenticatedProperty);
        set => SetValue(IsAuthenticatedProperty, value);
        
    }
    private readonly AuthService _authService;
    public HeaderContent()
	{
		InitializeComponent();
        _authService = new AuthService();
    }

    private void ToggleOptions(object sender, EventArgs e)
    {
        if (IsAuthenticated)
        {
            optionsStackNoAuth.IsVisible = false;
            // Show or hide the drop-down menu when the button is pressed
            optionsStackAuth.IsVisible = !optionsStackAuth.IsVisible;
        }
        else
        {
            optionsStackAuth.IsVisible = false;
            // Show or hide the drop-down menu when the button is pressed
            optionsStackNoAuth.IsVisible = !optionsStackNoAuth.IsVisible;
        }
    }

    private void CleanAll()
    {
        optionsStackAuth.IsVisible = false;
        optionsStackNoAuth.IsVisible = false;
    }

    private void GoToDashboard(object sender, EventArgs e)
    {
        // Action to go to dashboard
        Shell.Current.GoToAsync($"//{nameof(InfoPage)}");
        CleanAll();
    }

    private async void Logout(object sender, EventArgs e)
    {
        var token = await _authService.GetToken();
        await _authService.Logout(token);
        // Action to go to logout
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        CleanAll();
    }

    private void Login(object sender, EventArgs e)
    {
        // Action to go to login
        Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        CleanAll();
    }
}