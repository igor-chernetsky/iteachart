﻿using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class UserProfileModel
    {
        public UserProfileModel()
        {
            Skills = new List<UserProfileSkill>();
            KnownUsers = new List<UserProfileModel>();
        }
        public string UserName { get; set; }
        public string UserLastName { get; set; }

        public string UserFullName
        {
            get { return string.Format("{0} {1}", UserName, UserLastName); }
        }

        public int Age { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public List<UserProfileSkill> Skills { get; set; }
        public int Id { get; set; }
        public bool CanEdit { get; set; }
        public IEnumerable<UserProfileModel> KnownUsers { get; set; }
        public IEnumerable<AchievmentModel> Achievments { get; set; }
    }
}