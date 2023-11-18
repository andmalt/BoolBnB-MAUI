using BoolBnB_MAUI.Services;

namespace BoolBnB_MAUI.Pages;

public partial class InfoPage : ContentPage
{
    private readonly AuthService _authService;
    public InfoPage()
	{
		InitializeComponent();
        _authService = new AuthService();
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (await _authService.IsAuthenticatedAsync())
        {
            return;
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}