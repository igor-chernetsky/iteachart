using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class UserProfileModel
    {
        public UserProfileModel()
        {
            Skills = new List<UserProfileSkill>();
        }
        public string UserName { get; set; }
        public string Age { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public List<UserProfileSkill> Skills { get; set; } 
    }
}