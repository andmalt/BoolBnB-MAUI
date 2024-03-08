using BoolBnB_MAUI.Data.Entites;
using BoolBnB_MAUI.Data.Guest;
using BoolBnB_MAUI.Pages.Home;
using BoolBnB_MAUI.Services;
using BoolBnB_MAUI.Services.Interface;
using CommunityToolkit.Maui.Core.Extensions;
using System.Windows.Input;

namespace BoolBnB_MAUI.Pages.Homes;

public partial class HomesPage : ContentPage
{
	private readonly IHttpService _httpService;
	private readonly AuthService _authService;
    private bool isAuth;
    public ICommand ItemTappedCommand { get; }
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
        ItemTappedCommand = new Command<Apartment>(OnItemSelected);
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

        menuIcon.IsVisible = false;
        housesList.IsVisible = false;
        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;
        var response = await GetApartmentsAsync();
        if (response.Success)
        {
            housesList.ItemsSource = response.Apartments.Data.ToObservableCollection(); ;
            housesList.IsVisible = true;
        }        
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
        menuIcon.IsVisible = true;
    }

	protected async Task<HousesResponse> GetApartmentsAsync()
	{
		try
		{
            var response = await _httpService.Get<HousesResponse>("/api/homes-mobile");
            if (response.Success)
            {
                return response;
            }
            else
            {
                return new HousesResponse { Success = false, Apartments = { } };
            }
        }
        catch (HttpRequestException ex)
		{
            await DisplayAlert("Errore", "Imposibile ottenere dati, connessione internet mancante.", "Ok");
            Console.WriteLine($"ERROR (getApartments):{ex.Message}");
            throw;
        }

    }

    private async void OnItemSelected(Apartment apartment)
    {
        //Console.WriteLine($"Selected Object: {e.SelectedItem}");
        if (apartment != null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("Apartment", apartment);
            await Shell.Current.GoToAsync($"{nameof(HomePage)}", true, parameters);
        }
        // Deselect item
        housesList.SelectedItem = null;
    }
}