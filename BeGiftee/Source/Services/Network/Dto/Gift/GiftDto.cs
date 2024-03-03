
namespace BeGiftee.Source.Services.Network.Dto.Gift
{
    public class GiftDto
    {
        public bool archived { get; set; }
        public LabelDto[] labels { get; set; }
        public string _id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
