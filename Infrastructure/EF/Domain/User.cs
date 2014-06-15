using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models.Smg;

namespace Infrastructure.EF.Domain
{
    public class User : Employee
    {
        public int Id { get; set; }
        public virtual ICollection<UserSkill> AddedSkills { get; set; }
        public virtual ICollection<GuessedUser> GuessedUsers { get; set; }
        public virtual ICollection<GuessedUser> PlayedUsers { get; set; }

        public virtual ICollection<Attempt> Attempts { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<AchievmentAssigned> AchievmentsAssigned { get; set; } 
    }
}
