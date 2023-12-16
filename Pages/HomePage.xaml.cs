using BoolBnB_MAUI.Data.Entites;
using BoolBnB_MAUI.Data.Guest;
using BoolBnB_MAUI.Services;
using BoolBnB_MAUI.Services.Interface;

namespace BoolBnB_MAUI.Pages;

[QueryProperty(nameof(Apartment), "Apartment")]
public partial class HomePage : ContentPage
{
    private readonly IHttpService _httpService;
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
    public HomePage()
	{
		InitializeComponent();
        _httpService = new HttpService();
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