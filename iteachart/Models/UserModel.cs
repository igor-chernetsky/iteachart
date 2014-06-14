using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iteachart.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string Position { get; set; }
        public string Room { get; set; }
        public int DeptId { get; set; }
        public string Image { get; set; }
        public bool IsEnabled { get; set; }
        public string FullName { get { return FirstNameEng + " " + LastNameEng; } }

        public string ProfileLink { get { return "/Profile/Index/" + Id; } }
    }
}