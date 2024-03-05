using BeGiftee.Source.Helpers;
using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Api;

namespace BeGiftee.Source.ContentPages.Gifts;

public partial class GiftFormPage : ContentPage
{
    private IGiftService _giftService;
    private Gift giftToEdit;
    public GiftFormPage(Gift giftToEdit = null)
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

    private async void OnEditGiftButtonClicked(object sender, EventArgs e)
    {
        if (!IsFormValid())
        {
            await Application.Current.MainPage.DisplayAlert("Invalid form", "Fill in all required values", "Go back");
            return;
        }
        var giftWithChanges = new Gift { Id = giftToEdit.Id, Title = title.Text, Content = content.Text, Archived = giftToEdit.Archived, Labels = giftToEdit.Labels };
        try
        {
            await _giftService.EditGift(giftWithChanges);
            await Navigation.PopAsync();
        }
        catch (Exception ex)
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

    public bool IsEditMode()
    {
        return giftToEdit != null;
    }

    public bool IsAddMode()
    {
        return !IsEditMode();
    }
}