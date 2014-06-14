using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class UserProfileSkill
    {
        public string SkillName { get; set; }
        public List<UserProfileSubSkill> SubSkills { get; set; }

    }
}