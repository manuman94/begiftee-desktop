using BeGiftee.Source.Helpers;
using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Api;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace BeGiftee.Source.ViewModels
{
    internal class GiftEditViewModel: INotifyPropertyChanged
    {
        private IGiftService _giftService;
        private Gift _giftToEdit;

        public Gift giftToEdit
        {
            get => _giftToEdit;
            set
            {
                if (_giftToEdit != value)
                {
                    _giftToEdit = value;
                    OnPropertyChanged();
                }
            }
        }
        public GiftEditViewModel(Gift giftToEdit)
        {
            _giftToEdit = giftToEdit;
            _giftService = ServiceHelper.GetService<IGiftService>();
        }

        public async void EditGift(Gift giftWithChanges)
        {
            await _giftService.EditGift(giftWithChanges);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
