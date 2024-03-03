
namespace BeGiftee.Source.Models
{
    public class Gift
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public Label[] Labels { get; set; }
    }
}
