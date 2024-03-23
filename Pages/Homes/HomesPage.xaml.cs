using BoolBnB_MAUI.ViewModels;

namespace BoolBnB_MAUI.Pages.Homes;

public partial class HomesPage : ContentPage
{
    public HomesPage()
	{
		InitializeComponent();
        var viewModel = this.BindingContext as HomesViewModel;
        viewModel.RequestDisplayAlert += ViewModelRequestDisplayAlert;
    }

    private async void ViewModelRequestDisplayAlert(object sender, string e)
    {
        await DisplayAlert("Error", e, "OK");
    }
}