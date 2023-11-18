using BoolBnB_MAUI.Data.Entites;
using BoolBnB_MAUI.Data.Guest;
using BoolBnB_MAUI.Services;
using BoolBnB_MAUI.Services.Interface;
using System.Text.Json;

namespace BoolBnB_MAUI.Pages;

public partial class HomesPage : ContentPage
{
	private readonly IHttpService _httpService;
    private readonly JsonSerializerOptions _serializerOptions;
    public HomesPage()
	{
		InitializeComponent();
		_httpService = new HttpService();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

	protected async override void OnAppearing()
	{
		base.OnAppearing();
        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;
        var response = await GetApartmentsAsync();
        var apartments = response.Apartments.ToList();
        housesList.ItemsSource = apartments;
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
    }

	protected async Task<HousesResponse> GetApartmentsAsync()
	{
		try
		{
            HttpResponseMessage response = await _httpService.Get("/api/homes");
            var responseContent = await response.Content.ReadAsStringAsync();
            var content = JsonSerializer.Deserialize<HousesResponse>(responseContent, _serializerOptions);
            //Console.WriteLine($"Response => {content.Apartments.Count}");
            return content;
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
        //((ListView)sender).SelectedItem = null;
        var parameters = new Dictionary<string, object>();
        parameters.Add("Apartment", selectedApartment);
        await Shell.Current.GoToAsync($"//{nameof(HomePage)}", true, parameters);
    }
}