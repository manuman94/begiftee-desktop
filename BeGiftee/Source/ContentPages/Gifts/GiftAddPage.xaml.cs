using BeGiftee.Source.Helpers;
using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Api;

namespace BeGiftee.Source.ContentPages.Gifts;

public partial class GiftAddPage : ContentPage
{
    private IGiftService _giftService;
    public GiftAddPage()
    {
		InitializeComponent();
        _giftService = ServiceHelper.GetService<IGiftService>();
    }

    private async void OnAddGiftButtonClicked(object sender, EventArgs e)
    {
        if (!IsFormValid())
        {
            await Application.Current.MainPage.DisplayAlert("Invalid form", "Fill in all required values", "Go back");
            return;
        }
        var newGift = new Gift { Title = title.Text, Content = content.Text, Archived = false, Labels = [] };
        try
        {
            await _giftService.CreateGift(newGift);
            await Navigation.PopAsync();
        } catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Go back");
        }
    }

    private bool IsFormValid()
    {
        return !string.IsNullOrEmpty(title.Text) 
            && !string.IsNullOrEmpty(content.Text); // TODO create some validator class and reflect errors on the UI (although there is server validation)
    }

    private async void OnGoBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}