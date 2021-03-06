﻿using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class UserProfileSkill
    {
        public UserProfileSkill()
        {
            SubSkills = new List<UserProfileSubSkill>();
        }
        public string SkillName { get; set; }
        public IEnumerable<UserProfileSubSkill> SubSkills { get; set; }

    }
}