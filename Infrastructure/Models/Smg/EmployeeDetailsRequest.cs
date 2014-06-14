using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Smg
{
    public class EmployeeDetailsRequest : BaseRequest
    {
        public override string Method { get { return "GetEmployeeDetails"; } }

        public EmployeeDetailsRequest(string sessionId) : base(sessionId)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Method, base.ToString());
        }
    }
}
