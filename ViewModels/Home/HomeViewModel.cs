﻿using BoolBnB_MAUI.Data.Entites;
using BoolBnB_MAUI.Data.Guest;
using BoolBnB_MAUI.Services;
using BoolBnB_MAUI.Services.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BoolBnB_MAUI.ViewModels.Home
{
    [QueryProperty(nameof(Apartment), "Apartment")]
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IHttpService _httpService;
        private readonly AuthService _authService;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool isAuth;

        [ObservableProperty]
        private Apartment apartment;

        public HomeViewModel() 
        {
            _httpService = new HttpService();
            _authService = new AuthService();
        }

        [RelayCommand]
        public async Task Appearing()
        {
            IsLoading = true;
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
                
                await GetApartment(Apartment);

                IsLoading = false;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR Appearing(getApartment):{ex.Message}");
            }
            
        }

        protected async Task GetApartment(Apartment apartment)
        {
            //Console.WriteLine($"Apartment ID: {apartment.Id}");
            try
            {
                var response = await _httpService.Post<HouseResponse>($"/api/house/{apartment.Id}");
                if (response.Success)
                {
                    Apartment = response.Apartment;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ERROR (getApartment):{ex.Message}");
            }
        }
    }
}
