using BoolBnB_MAUI.Services;

namespace BoolBnB_MAUI.Pages;

public partial class InfoPage : ContentPage
{
    private readonly AuthService _authService;
    private bool IsAuth {  get; set; }
    public InfoPage()
	{
		InitializeComponent();
        _authService = new AuthService();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if (await _authService.IsAuthenticatedAsync())
        {
            IsAuth = true;
        }
        else
        {
            IsAuth = false;
        }
    }
}