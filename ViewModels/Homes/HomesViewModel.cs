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

namespace BoolBnB_MAUI.ViewModels.Homes;

public partial class HomesViewModel : ObservableObject
{
    private readonly IHttpService _httpService;
    private readonly AuthService _authService;

    public event EventHandler<string> RequestDisplayAlert;

    [ObservableProperty]
    private ObservableCollection<Apartment> apartments;

    [ObservableProperty]
    private bool isAuth;

    [ObservableProperty]
    private bool isVisible;

    [ObservableProperty]
    private bool isVisibleLoading;

    [ObservableProperty]
    private int currentPage;

    [ObservableProperty]
    private int lastPage;

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

            IsVisible = false;
            IsVisibleLoading = true;
            var response = await GetApartmentsAsync();
            if (response.Success)
            {
                LastPage = response.Apartments.LastPage;
                CurrentPage = response.Apartments.CurrentPage;
                Apartments = response.Apartments.Data.ToObservableCollection();
                IsVisible = true;
            }
            IsVisibleLoading = false;
        }
        catch(Exception ex)
        {
            var innerException = ex.InnerException?.Message;
            var error = string.IsNullOrEmpty(innerException) ? ex.Message : innerException;
            IsVisibleLoading = false;
            IsVisible = true;
            OnRequestDisplayAlert($"Unable to get data, network connection lost, check your connection.\nError: {error}");
            //System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
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

    [RelayCommand]
    public async Task PreviousPage()
    {
        if (CurrentPage <= 1)
        {
            return;
        }

        try
        {               
            CurrentPage--;
            await Pagination($"/api/homes-mobile?page={CurrentPage}");
        }
        catch (Exception ex)
        {
            CurrentPage++;
            OnRequestDisplayAlert($"There is an error that in changing page , check connection.\nError: {ex.Message}");
        }


    }

    [RelayCommand]
    public async Task NextPage()
    {
        if (CurrentPage >= LastPage)
        {
            return;
        }

        try
        {
            CurrentPage++;
            await Pagination($"/api/homes-mobile?page={CurrentPage}");
        }
        catch (Exception ex)
        {
            CurrentPage--;
            OnRequestDisplayAlert($"There is an error that in changing page , check connection.\nError: {ex.Message}");
        }
    }

    [RelayCommand]
    public async Task FirstPage()
    {
        if (CurrentPage <= 1)
        {
            return;
        }

        try
        {
            await Pagination($"/api/homes-mobile?page=1");
        }
        catch (Exception ex)
        {
            OnRequestDisplayAlert($"There is an error that in changing page , check connection.\nError: {ex.Message}");
        }
    }

    [RelayCommand]
    public async Task Last_Page()
    {
        if (CurrentPage >= LastPage)
        {
            return;
        }

        try
        {
            await Pagination($"/api/homes-mobile?page={LastPage}");
        }
        catch (Exception ex)
        {
            OnRequestDisplayAlert($"There is an error that in changing page , check connection.\nError: {ex.Message}");
        }
    }

    protected virtual void OnRequestDisplayAlert(string message)
    {
        RequestDisplayAlert?.Invoke(this,message);
    }

    private async Task Pagination(string url)
    {
        try
        {
            IsVisible = false;
            IsVisibleLoading = true;
            var response = await _httpService.Get<HousesResponse>(url);
            if (response.Success)
            {
                LastPage = response.Apartments.LastPage;
                CurrentPage = response.Apartments.CurrentPage;
                Apartments = response.Apartments.Data.ToObservableCollection();
                IsVisible = true;
            }
            IsVisibleLoading = false;
        }
        catch(Exception ex)
        {
            var innerException = ex.InnerException?.Message;
            var error = string.IsNullOrEmpty(innerException) ? ex.Message : innerException;
            IsVisibleLoading = false;
            IsVisible = true;
            OnRequestDisplayAlert($"There is an error in the pagination.\nError: {error}");
        }
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
            Console.WriteLine($"httpRequest error: {ex.Message}");
            OnRequestDisplayAlert("Unable to get data, network connection lost.");
            return null;
        }

    }
}
