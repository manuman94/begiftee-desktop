using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Network.Dto.Gift;

namespace BeGiftee.Source.Services.Network.Mappers
{
    public class GiftDtoToGiftMapper : IMapper<GiftDto, Gift>
    {
        public Gift Map(GiftDto giftDto)
        {
            return new Gift
            {
                Id = giftDto._id,
                Title = giftDto.Title,
                Content = giftDto.Content,
                CreatedAt = giftDto.CreatedAt,
                UpdatedAt = giftDto.UpdatedAt,
                Labels = giftDto.Labels.Select(labelDto => new LabelDtoToLabelMapper().Map(labelDto)).ToArray(),
            };
        }
    }
}
