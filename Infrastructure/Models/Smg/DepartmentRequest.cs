using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Smg
{
    public class DepartmentRequest : BaseRequest
    {
        public override string Method { get { return "GetAllDepartments"; }}

        public DepartmentRequest(string sessionId) : base(sessionId)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Method, base.ToString());
        }
    }
}
