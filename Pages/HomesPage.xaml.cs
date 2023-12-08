using BoolBnB_MAUI.Data.Entites;
using BoolBnB_MAUI.Data.Guest;
using BoolBnB_MAUI.Services;
using BoolBnB_MAUI.Services.Interface;
using System.Text.Json;

namespace BoolBnB_MAUI.Pages;

public partial class HomesPage : ContentPage
{
	private readonly IHttpService _httpService;
	private readonly AuthService _authService;
    private bool isAuth;
    public bool IsAuth
    {
        get => isAuth;
        set
        {
            isAuth = value;
            OnPropertyChanged(nameof(IsAuth));
            EnableBinding();
        }
    }
    public HomesPage()
	{
		InitializeComponent();
		_httpService = new HttpService();
        _authService = new AuthService();
    }

    private void EnableBinding()
    {
        BindingContext = this;
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

        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;
        var response = await GetApartmentsAsync();
        if (response.Success)
        {
            var apartments = response.Apartments.ToList();
            housesList.ItemsSource = apartments;
        }        
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
        menuIcon.IsVisible = true;
    }

	protected async Task<HousesResponse> GetApartmentsAsync()
	{
		try
		{
            var response = await _httpService.Get<HousesResponse>("/api/homes");
            if (response.Success)
            {
                return response;
            }
            else
            {
                return new HousesResponse { Success = false, Apartments = { }, Photos = { } };
            }
        }
        catch (HttpRequestException ex)
		{
            Console.WriteLine($"ERROR (getApartments):{ex.Message}");
            throw;
        }

    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Console.WriteLine($"Selected Object: {e.SelectedItem}");
        if (e.SelectedItem == null)
            return;

        var selectedApartment = e.SelectedItem as Apartment;
        //DisplayAlert("Selezione", $"Hai selezionato {selectedApartment.Title}", "OK");

        // Deseleziona l'elemento
        ((ListView)sender).SelectedItem = null;
        var parameters = new Dictionary<string, object>();
        parameters.Add("Apartment", selectedApartment);
        await Shell.Current.GoToAsync($"//{nameof(HomePage)}", true, parameters);
    }
}