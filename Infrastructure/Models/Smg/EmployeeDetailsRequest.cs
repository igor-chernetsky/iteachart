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
        public int ProfileId { get; set; }

        public EmployeeDetailsRequest(string sessionId, int profileId) : base(sessionId)
        {
            ProfileId = profileId;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}&profileId={2}", Method, base.ToString(), ProfileId);
        }
    }
}
