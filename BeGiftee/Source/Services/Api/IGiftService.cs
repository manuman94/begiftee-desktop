using BeGiftee.Source.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeGiftee.Source.Services.Api
{
    public interface IGiftService
    {
        Task<Gift[]> GetAllMyGifts();
        Task<Gift> CreateGift(Gift gift);
        Task<Gift> EditGift(string giftId);
        Task DeleteGift(string giftId);
    }
}
