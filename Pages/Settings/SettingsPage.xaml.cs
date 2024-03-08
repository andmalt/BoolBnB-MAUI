using BoolBnB_MAUI.Pages.Main;
using BoolBnB_MAUI.Services;

namespace BoolBnB_MAUI.Pages.Settings;

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

		Token = await _authService.GetToken();
		if (Token == null)
		{
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
		
    }

	private async void GoToMain(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
	}
}