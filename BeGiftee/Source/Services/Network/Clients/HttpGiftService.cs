using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Api;
using BeGiftee.Source.Services.Network.Dto.Gift;
using BeGiftee.Source.Services.Network.Mappers;
using System.Diagnostics;

namespace BeGiftee.Source.Services.Network.Clients
{
    internal class HttpGiftService(HttpService httpService) : IGiftService
    {
        private readonly HttpService _httpService = httpService;

        public async Task<Gift> CreateGift(Gift gift)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteGift(string giftId)
        {
            throw new NotImplementedException();
        }

        public async Task<Gift> EditGift(string giftId)
        {
            throw new NotImplementedException();
        }

        public async Task<Gift[]> GetAllMyGifts()
        {
            Trace.WriteLine("HttpGiftService -> GetAllMyGifts");
            return (await _httpService.GetAsync<GiftDto[]>(ApiEndpoints.GetAllMyGifts))
                .Select(giftDto => new GiftDtoToGiftMapper().Map(giftDto)).ToArray();
        }
    }
}
