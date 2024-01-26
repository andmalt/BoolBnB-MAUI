using BoolBnB_MAUI.Data.Entites;
using BoolBnB_MAUI.Data.Guest;
using BoolBnB_MAUI.Services;
using BoolBnB_MAUI.Services.Interface;

namespace BoolBnB_MAUI.Pages;

[QueryProperty(nameof(Apartment), "Apartment")]
public partial class HomePage : ContentPage
{
    private readonly IHttpService _httpService;
    private readonly AuthService _authService;
    private Apartment apartment {  get; set; }
    public Apartment Apartment
    { 
        get => apartment;
        set
        {
            OnPropertyChanged(nameof(Apartment));
            AddHouse(value);
        }
    }

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
    public HomePage()
	{
		InitializeComponent();
        _httpService = new HttpService();
        _authService = new AuthService();
    }

    private void EnableBinding()
    {
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine("Appearing");

        if (await _authService.IsAuthenticatedAsync())
        {
            IsAuth = true;
        }
        else
        {
            IsAuth = false;
        }
    }

    protected void AddHouse(Apartment apartment)
    {
        if (apartment == null)
        {
            Console.WriteLine("No House here!!!");
            return;
        }
        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;
        GetApartment(apartment);
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
    }

    protected async void GetApartment(Apartment apartment)
    {
        Console.WriteLine($"Apartment ID: {apartment.Id}");
        try
        {
            var response = await _httpService.Post<HouseResponse>($"/api/house/{apartment.Id}");
            if (response.Success)
            {
                BindingContext = response.Apartment;
                apartment = response.Apartment;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"ERROR (getApartments):{ex.Message}");
            throw;
        }
    }
}