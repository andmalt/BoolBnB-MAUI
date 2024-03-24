using BoolBnB_MAUI.ViewModels.Homes;

namespace BoolBnB_MAUI.Pages.Homes;

public partial class HomesPage : ContentPage
{
    public HomesPage()
	{
		InitializeComponent();   
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ConfigureViewModelEventHandlers(true);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ConfigureViewModelEventHandlers(false);
    }

    private void ConfigureViewModelEventHandlers(bool subscribe)
    {
        if (BindingContext is HomesViewModel viewModel)
        {
            if (subscribe)
            {
                viewModel.RequestDisplayAlert += ViewModelRequestDisplayAlert;
            }
            else
            {
                viewModel.RequestDisplayAlert -= ViewModelRequestDisplayAlert;
            }
        }
    }

    private async void ViewModelRequestDisplayAlert(object sender, string e)
    {
        await DisplayAlert("Error", e, "OK");
    }
}