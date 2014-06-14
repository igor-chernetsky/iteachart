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
        public string Email { get; set; }
        public string DomenName { get; set; }
        public int DepId { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string Room { get; set; }
    }
}
