using BeGiftee.Source.Pages.Register;

namespace BeGiftee.Source.Views.Auth;

public partial class LoginView : ContentView
{
	public LoginView()
	{
		InitializeComponent();
	}
    private async void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}