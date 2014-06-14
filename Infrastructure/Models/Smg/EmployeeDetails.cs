using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Smg
{
    public class EmployeeDetailsResponse : BaseResponse
    {
        public EmployeeDetails Profile { get; set; }
    }

    public class EmployeeDetails
    {
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }
        public string DomenName { get; set; }
        public string Rank { get; set; }
        public string Email { get; set; }
    }
}
