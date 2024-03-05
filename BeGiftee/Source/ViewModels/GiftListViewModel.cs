using BeGiftee.Source.ContentPages.Gifts;
using BeGiftee.Source.Helpers;
using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Api;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeGiftee.Source.ViewModels
{
    public class GiftListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Gift> Gifts { get; set; }
        IGiftService _giftService;

        public GiftListViewModel()
        {
            _giftService = ServiceHelper.GetService<IGiftService>();
        }

        public async Task LoadMyGifts()
        {
            Gifts = new ObservableCollection<Gift>(await _giftService.GetAllMyGifts());
            OnPropertyChanged(nameof(Gifts));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void EditGift(Gift gift)
        {
        }

        public async void RemoveGift(Gift gift)
        {
            try
            {
                await _giftService.DeleteGift(gift.Id);
                await LoadMyGifts();
                OnPropertyChanged(nameof(Gifts));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error deleting gift", ex.Message, "Go back");
            }
        }
    }
}
