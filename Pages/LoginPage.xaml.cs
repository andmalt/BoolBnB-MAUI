using BoolBnB_MAUI.Services;

namespace BoolBnB_MAUI.Pages;

public partial class LoginPage : ContentPage
{
    protected readonly AuthService _authService;
    private string _email;
    private string _password;
    public LoginPage()
	{
		InitializeComponent();
       _authService = new AuthService();
        errPass.IsVisible = false;
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
}