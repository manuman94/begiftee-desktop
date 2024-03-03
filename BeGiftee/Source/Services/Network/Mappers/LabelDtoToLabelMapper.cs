
using BeGiftee.Source.Models;
using BeGiftee.Source.Services.Network.Dto.Gift;
using Label = BeGiftee.Source.Models.Label;

namespace BeGiftee.Source.Services.Network.Mappers
{
    internal class LabelDtoToLabelMapper : IMapper<LabelDto, Label>
    {
        public Label Map(LabelDto labelDto)
        {
            return new Label
            {
                Name = labelDto.Name,
            };
        }
    }
}
