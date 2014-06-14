using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Secure
{
    [Serializable]
    public class SessionUser
    {
        public string UserName { get; set; }
        public string AuthorizationToken { get; set; }
    }
}
