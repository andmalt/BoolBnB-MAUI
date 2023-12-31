using BoolBnB_MAUI.Services;

namespace BoolBnB_MAUI.Pages
{
    public partial class MainPage : ContentPage
    {
        private bool isAuth;
        public bool IsAuth {
            get => isAuth;
            set
            {
                isAuth = value;
                OnPropertyChanged(nameof(IsAuth));
                EnableBinding();
            }
        }
        private readonly AuthService _authService;
        public MainPage()
        {
            InitializeComponent();
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

        protected override async void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            base.OnNavigatingFrom(args);
            Console.WriteLine("Navigated");

            if (await _authService.IsAuthenticatedAsync())
            {
                IsAuth = true;
            }
            else
            {
                IsAuth = false;
            }
        }

        private async void GoToHomesPages(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomesPage)}");
        }
    }
}