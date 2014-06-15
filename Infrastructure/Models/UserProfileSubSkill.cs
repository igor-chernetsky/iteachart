using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class UserProfileSubSkill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public bool IsApproved { get; set; }

        public IEnumerable<RatingItem> Raiting { get; set; }
    }
}