
namespace BeGiftee.Source.Services.Network.Dto.Gift
{
    public class GiftDto
    {
        public bool Archived { get; set; }
        public LabelDto[] Labels { get; set; }
        public string _id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
