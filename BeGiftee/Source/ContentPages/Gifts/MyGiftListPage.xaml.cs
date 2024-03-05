using BeGiftee.Source.ContentPages.Gifts;
using BeGiftee.Source.Helpers;
using BeGiftee.Source.Models;
using BeGiftee.Source.Pages.Auth.ForgotPassword;
using BeGiftee.Source.Services.Api;
using BeGiftee.Source.ViewModels;

namespace BeGiftee.Source.Pages;

public partial class MyGiftListPage : ContentPage
{
    private IAuthenticationService _authenticationService;
    public MyGiftListPage()
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

    private async void OnNewGiftButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GiftFormPage());
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((GiftListViewModel)BindingContext)?.LoadMyGifts();
    }

    public async void OnEditGiftButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var giftToEdit = (Gift)button.CommandParameter;
        await Navigation.PushAsync(new GiftFormPage(giftToEdit));
    }

    public async void OnRemoveGiftButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var gift = (Gift)button.CommandParameter;
        ((GiftListViewModel)BindingContext).RemoveGift(gift);
    }
}