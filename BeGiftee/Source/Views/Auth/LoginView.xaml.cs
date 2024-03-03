using BeGiftee.Source.Helpers;
using BeGiftee.Source.Pages.Register;
using BeGiftee.Source.Services.Network.Clients;
using BeGiftee.Source.Pages;
using BeGiftee.Source.Pages.Auth.ForgotPassword;

namespace BeGiftee.Source.Views.Auth;

public partial class LoginView : ContentView
{
    private IAuthenticationService _authenticationService;
	public LoginView()
    {
		InitializeComponent();
        _authenticationService = ServiceHelper.GetService<IAuthenticationService>();
    }

    private async void OnSignInButtonClicked(object sender, EventArgs e)
    {
        try
        {
            await _authenticationService.Login(username.Text, password.Text);
            Application.Current.MainPage = new NavigationPage(new MyGiftList());
            await Navigation.PopToRootAsync();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("(DEBUG) Login incorrect", ex.Message, "Go back");
        }
    }

    private async void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    private async void OnForgotPasswordButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ForgotPasswordPage());
    }
}