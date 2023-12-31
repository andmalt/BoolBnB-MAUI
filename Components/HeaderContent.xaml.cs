using BoolBnB_MAUI.Pages;

namespace BoolBnB_MAUI.Components;

public partial class HeaderContent : ContentView
{
    public static readonly BindableProperty IsAuthenticatedProperty =
            BindableProperty.Create(nameof(IsAuthenticated), typeof(bool), typeof(HeaderContent), default(bool),
                propertyChanged: OnIsAuthenticatedChanged);
    public static readonly BindableProperty BackArrowProperty =
            BindableProperty.Create(nameof(BackArrow), typeof(bool), typeof(HeaderContent));

    public bool BackArrow
    {
        get => (bool)GetValue(BackArrowProperty);
        set => SetValue(BackArrowProperty, value);
        
    }
    public bool IsAuthenticated
    {
        get => (bool)GetValue(IsAuthenticatedProperty);
        set => SetValue(IsAuthenticatedProperty, value);
    }

    public HeaderContent()
	{
		InitializeComponent();
        EnableChangedProperty();
        btnBack.IsVisible = BackArrow;
    }

    private static void OnIsAuthenticatedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HeaderContent)bindable;
        control.EnableChangedProperty();
    }

    private void EnableChangedProperty()
    {
        if (IsAuthenticated)
        {
            btnAuth.IsVisible = true;
            btnNoAuth.IsVisible = false;
        }
        else
        {
            btnAuth.IsVisible = false;
            btnNoAuth.IsVisible = true;
        }
    }

    private void GoBackToMain(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

    private void GoToDashboard(object sender, EventArgs e)
    {
        
        // Action to go to dashboard
        Shell.Current.GoToAsync($"//{nameof(InfoPage)}");
    }

    private void Login(object sender, EventArgs e)
    {
        // Action to go to login
        Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    private void PressedDashboard(object sender, EventArgs e)
    {
        if(Application.Current.Resources.TryGetValue("LightLink", out var pressedColor))
        {
            labelDashboard.TextColor = (Color)pressedColor;
        }
        imageDashboard.HeightRequest = 18.0;
        imageDashboard.WidthRequest = 18.0;
    }

    private void ReleasedDashboard(object sender, EventArgs e)
    {
        if (Application.Current.Resources.TryGetValue("Link", out var defaultColor))
        {
            labelDashboard.TextColor = (Color)defaultColor;
        }
        imageDashboard.HeightRequest = 24.0;
        imageDashboard.WidthRequest = 24.0;
        GoToDashboard(sender, e);
    }

    private void PressedLogin(object sender, EventArgs e)
    {
        if (Application.Current.Resources.TryGetValue("LightLink", out var pressedColor))
        {
            labelLogin.TextColor = (Color)pressedColor;
        }
        imageLogin.HeightRequest = 18.0;
        imageLogin.WidthRequest = 18.0;
    }

    private void ReleasedLogin(object sender, EventArgs e)
    {
        if (Application.Current.Resources.TryGetValue("Link", out var defaultColor))
        {
            labelLogin.TextColor = (Color)defaultColor;
        }
        imageLogin.HeightRequest = 24.0;
        imageLogin.WidthRequest = 24.0;
        GoToDashboard(sender, e);
    }
}