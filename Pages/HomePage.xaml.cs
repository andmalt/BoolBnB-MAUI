using BoolBnB_MAUI.Data.Entites;
using BoolBnB_MAUI.Data.Guest;
using BoolBnB_MAUI.Services;
using BoolBnB_MAUI.Services.Interface;
using System.Text.Json;

namespace BoolBnB_MAUI.Pages;

[QueryProperty(nameof(Apartment), "Apartment")]
public partial class HomePage : ContentPage
{
    private readonly IHttpService _httpService;
    private readonly JsonSerializerOptions _serializerOptions;
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
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
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
            HttpResponseMessage response = await _httpService.Post($"/api/house/{apartment.Id}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var content = JsonSerializer.Deserialize<HouseResponse>(responseContent, _serializerOptions);
            //Console.WriteLine($"Response => {content.Apartments.Count}");
            if (content.Success)
            {
                BindingContext = content.Apartment;
                apartment = content.Apartment;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"ERROR (getApartments):{ex.Message}");
            throw;
        }
    }
}