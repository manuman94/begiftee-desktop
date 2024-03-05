using BeGiftee.Source.Helpers;
using BeGiftee.Source.Services.Api;

namespace BeGiftee.Source.Pages.Auth.ForgotPassword;

public partial class ForgotPasswordPage : ContentPage
{
    private IAuthenticationService _authenticationService;
    public ForgotPasswordPage()
    {
        InitializeComponent();
        _authenticationService = ServiceHelper.GetService<IAuthenticationService>();
    }

    private async void OnRecoverButtonClicked(object sender, EventArgs e)
    {
        try
        {
            await _authenticationService.RecoverPassword(email.Text);
            await Application.Current.MainPage.DisplayAlert("Password recovery", "A recovery link has been sent to your email address", "Okay!");
            await Navigation.PopToRootAsync();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Failed asking for the recovery email", ex.Message, "Go back");
        }
    }

    private async void OnGoBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}