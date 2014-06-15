using Infrastructure.Code;

namespace Infrastructure.Models
{
    public class AchievmentModel
    {
        public string Description { get; set; }
        public BadgeType BadgeType { get; set; }
        public string ImageUrl { get; set; }
    }
}