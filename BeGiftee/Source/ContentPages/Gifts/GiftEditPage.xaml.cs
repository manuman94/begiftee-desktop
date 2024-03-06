using BeGiftee.Source.Helpers;
using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Api;
using BeGiftee.Source.ViewModels;

namespace BeGiftee.Source.ContentPages.Gifts;

public partial class GiftEditPage : ContentPage
{
    private Gift _giftToEdit;
    public GiftEditPage(Gift giftToEdit = null)
	{
        InitializeComponent();
        BindingContext = new GiftEditViewModel(giftToEdit);
        _giftToEdit = giftToEdit;
    }

    private bool IsFormValid()
    {
        return !string.IsNullOrEmpty(title.Text)
            && !string.IsNullOrEmpty(content.Text); // TODO create some validator class and reflect errors on the UI (although there is server validation)
    }

    private async void OnEditGiftButtonClicked(object sender, EventArgs e)
    {
        if (!IsFormValid())
        {
            await Application.Current.MainPage.DisplayAlert("Invalid form", "Fill in all required values", "Go back");
            return;
        }
        var giftWithChanges = new Gift { Id = _giftToEdit.Id, Title = title.Text, Content = content.Text, 
            Archived = _giftToEdit.Archived, Labels = _giftToEdit.Labels };
        try
        {
            ((GiftEditViewModel)BindingContext).EditGift(giftWithChanges);
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Go back");
        }
    }

    private async void OnGoBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}