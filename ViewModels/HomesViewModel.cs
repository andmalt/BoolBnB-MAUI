using BoolBnB_MAUI.Data.Entites;
using BoolBnB_MAUI.Data.Guest;
using BoolBnB_MAUI.Pages.Home;
using BoolBnB_MAUI.Services;
using BoolBnB_MAUI.Services.Interface;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace BoolBnB_MAUI.ViewModels
{
    public partial class HomesViewModel : ObservableObject
    {
        private readonly IHttpService _httpService;
        private readonly AuthService _authService;

        [ObservableProperty]
        public ObservableCollection<Apartment> apartments;

        [ObservableProperty]
        public bool isAuth;

        [ObservableProperty]
        public bool isVisibleLoading;

        [ObservableProperty]
        public bool isVisibleMenu;

        [ObservableProperty]
        public bool isVisibleList;

        public HomesViewModel()
        {
            _httpService = new HttpService();
            _authService = new AuthService();
        }

        [RelayCommand]
        public async Task Appearing()
        {
            try
            {
                if (await _authService.IsAuthenticatedAsync())
                {
                    IsAuth = true;
                }
                else
                {
                    IsAuth = false;
                }

                IsVisibleMenu = false;
                IsVisibleList = false;
                IsVisibleLoading = true;
                var response = await GetApartmentsAsync();
                if (response.Success)
                {
                    Apartments = response.Apartments.Data.ToObservableCollection(); ;
                    IsVisibleList = true;
                }
                IsVisibleLoading = false;
                IsVisibleMenu = true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        public async Task ItemSelected(Apartment apartment)
        {
            //Console.WriteLine($"Selected Object: {e.SelectedItem}");
            if (apartment != null)
            {
                var parameters = new Dictionary<string, object>();
                parameters.Add("Apartment", apartment);
                await Shell.Current.GoToAsync($"{nameof(HomePage)}", true, parameters);
            }
            // Deselect item
            //housesList.SelectedItem = null;
        }

        private async Task<HousesResponse> GetApartmentsAsync()
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
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                // Handle invalid JSON
                return null;
            }
            catch (HttpRequestException ex)
            {
                //DisplayAlert("Errore", "Imposibile ottenere dati, connessione internet mancante.", "Ok");
                Console.WriteLine($"ERROR (getApartments):{ex.Message}");
                throw;
            }

        }
    }
}
 