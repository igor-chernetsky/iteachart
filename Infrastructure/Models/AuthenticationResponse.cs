using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class AuthenticationResponse
    {
        public string ErrorCode { get; set; }
        public string Persmission { get; set; }
        public string SessionId { get; set; }
    }
}
