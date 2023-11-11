using BoolBnB_MAUI.Services;

namespace BoolBnB_MAUI.Pages;

public partial class SettingsPage : ContentPage
{
	protected readonly AuthService _authService;
	private string Token {  get; set; }
	public SettingsPage()
	{
		InitializeComponent();
		_authService = new AuthService();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

		Token = await AuthService.GetToken();
		if (Token == null)
		{
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
		
    }

    private async void Logout(object sender, EventArgs e)
	{
		var isLogout = await _authService.Logout(Token); 
		if (isLogout)
		{
			//
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
		else
		{
			//
		}
	}
}