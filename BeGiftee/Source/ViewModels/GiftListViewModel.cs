using BeGiftee.Source.Helpers;
using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Api;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Label = BeGiftee.Source.Models.Label;

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
            Trace.WriteLine("GiftListViewModel -> LoadMyGifts");
            try
            {
                Gifts = new ObservableCollection<Gift>(await _giftService.GetAllMyGifts());
                OnPropertyChanged("Gifts");
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
