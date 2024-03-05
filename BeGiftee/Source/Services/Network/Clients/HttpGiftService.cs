using BeGiftee.Source.Helpers;
using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Api;
using BeGiftee.Source.Services.Network.Dto.Generic;
using BeGiftee.Source.Services.Network.Dto.Gift;
using BeGiftee.Source.Services.Network.Mappers;

namespace BeGiftee.Source.Services.Network.Clients
{
    internal class HttpGiftService(HttpService httpService) : IGiftService
    {
        private readonly HttpService _httpService = httpService;

        public async Task<Gift[]> GetAllMyGifts()
        {
            return (await _httpService.GetAsync<GiftDto[]>(ApiEndpoints.GetAllMyGifts))
                .Select(giftDto => new GiftDtoToGiftMapper().Map(giftDto)).ToArray();
        }

        public async Task<Gift> CreateGift(Gift gift)
        {
            return new GiftDtoToGiftMapper().Map((
                await _httpService.PostAsync<GiftDto>(ApiEndpoints.CreateGift, gift)));
        }

        public async Task<Gift> EditGift(Gift gift)
        {
            var url = UrlParamsHelper.GetUrlWithParams(ApiEndpoints.EditGift, gift.Id);
            return new GiftDtoToGiftMapper().Map((
                await _httpService.PatchAsync<GiftDto>(url, gift)));
        }

        public async Task DeleteGift(string giftId)
        {
            var url = UrlParamsHelper.GetUrlWithParams(ApiEndpoints.DeleteGift, giftId);
            await _httpService.DeleteAsync<EmptyResponse>(url);
        }
    }
}
