using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Smg
{
    public class BaseRequest
    {
        public string SessionId { get; private set; }
        public virtual string Method { get; set; }
        public BaseRequest(string sessionId)
        {
            SessionId = sessionId;
        }

        public override string ToString()
        {
            return "?sessionId=" + SessionId;
        }
    }
}
