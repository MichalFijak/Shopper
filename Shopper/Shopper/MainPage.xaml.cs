namespace Shopper;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(1500);

        // Fade-out
        await SplashView.FadeTo(0, 500);
        blazorWebView.IsVisible = true;
    }
}
