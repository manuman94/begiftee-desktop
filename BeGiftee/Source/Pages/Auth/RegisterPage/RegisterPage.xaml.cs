using BeGiftee.Source.Helpers;
using BeGiftee.Source.Services.Api;

namespace BeGiftee.Source.Pages.Register;

public partial class RegisterPage : ContentPage
{
    private IAuthenticationService _authenticationService;
    public RegisterPage()
	{
		InitializeComponent();
        _authenticationService = ServiceHelper.GetService<IAuthenticationService>();
    }
    
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        try
        {
            await _authenticationService.Register(email.Text, username.Text, password.Text);
            Application.Current.MainPage = new NavigationPage(new MyGiftList());
            await Navigation.PopToRootAsync();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("(DEBUG) Login incorrect", ex.Message, "Go back");
        }
    }

    private async void OnGoBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}