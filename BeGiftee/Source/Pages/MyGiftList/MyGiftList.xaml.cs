using BeGiftee.Source.Helpers;
using BeGiftee.Source.Services.Network.Clients;

namespace BeGiftee.Source.Pages;

public partial class MyGiftList : ContentPage
{
    private IAuthenticationService _authenticationService;
    public MyGiftList()
	{
		InitializeComponent();
        _authenticationService = ServiceHelper.GetService<IAuthenticationService>();
    }

    private async void OnLogOutButtonClicked(object sender, EventArgs e)
    {
        _authenticationService.Logout();
        Application.Current.MainPage = new NavigationPage(new MainPage());
        await Navigation.PopToRootAsync();
    }
}