using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Smg
{

    public class EmployeeResponse : BaseResponse
    {
        public IList<Employee> Profiles { get; set; }
    }

    public class Employee : EmployeeDetails
    {
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

        [NotMapped]
        public string FullName { get { return FirstNameEng + " " + LastNameEng; } }
    }
}
