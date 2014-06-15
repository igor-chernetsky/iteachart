using System.Collections.Generic;
using Infrastructure.Code;

namespace Infrastructure.EF.Domain
{
    public class Achievment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public BadgeType BadgeType { get; set; }
        public virtual ICollection<AchievmentAssigned> UsersAssigned { get; set; }
    }
}