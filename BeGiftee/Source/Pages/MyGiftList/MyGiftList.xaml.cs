using BeGiftee.Source.Helpers;
using BeGiftee.Source.Services.Api;
using BeGiftee.Source.ViewModels;
using System.Diagnostics;

namespace BeGiftee.Source.Pages;

public partial class MyGiftList : ContentPage
{
    private IAuthenticationService _authenticationService;
    public MyGiftList()
	{
		InitializeComponent();
        _authenticationService = ServiceHelper.GetService<IAuthenticationService>();
        BindingContext = new GiftListViewModel();
    }

    private async void OnLogOutButtonClicked(object sender, EventArgs e)
    {
        _authenticationService.Logout();
        Application.Current.MainPage = new NavigationPage(new MainPage());
        await Navigation.PopToRootAsync();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Trace.WriteLine("ON APPEARING!!!!!!!");
        await ((GiftListViewModel)BindingContext)?.LoadMyGifts();
    }
}