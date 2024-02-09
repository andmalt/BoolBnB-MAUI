using BoolBnB_MAUI.Services;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace BoolBnB_MAUI.Pages;

public partial class LoginPage : ContentPage
{
    protected readonly AuthService _authService;
    private string _email;
    private string _password;
    public bool IsPassword { get; set; } = true;
    public ICommand TogglePasswordVisibilityCommand {  get; }
    public LoginPage()
	{
		InitializeComponent();
       _authService = new AuthService();
        TogglePasswordVisibilityCommand = new Command(TogglePassword);
        errPass.IsVisible = false;
        BindingContext = this;
    }

    private void TogglePassword()
    {
        IsPassword = !IsPassword;
        eyeImage.Source = IsPassword ? "eye_slash.png" : "eye.png";
        Password.IsPassword = IsPassword;
    }

    public async void Login(object sender, EventArgs e)
    {
        bool isLogin = await _authService.Login(_email,_password);
        if (isLogin)
        {
            errPass.IsVisible = false;
            await Shell.Current.GoToAsync($"//{nameof(InfoPage)}");
        }
        else
        {
            errPass.IsVisible = true;
            await Task.Delay(3000);
            errPass.IsVisible = false;
        }
    }
    private void OnEmailChanged(object sender, TextChangedEventArgs e)
    {
        _email = e.NewTextValue;
    }

    private void OnPasswordChanged(object sender, TextChangedEventArgs e)
    {
        _password = e.NewTextValue;
    }

    private void EntryFocused(object sender, FocusEventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            formLayout.TranslateTo(0, -200, 50);
        }
    }

    private void EntryUnfocused(object sender, FocusEventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            formLayout.TranslateTo(0, 0, 50);
        }
    }
}