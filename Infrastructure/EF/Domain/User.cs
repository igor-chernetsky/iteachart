using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models.Smg;

namespace Infrastructure.EF.Domain
{
    public class User : Employee
    {
        public int Id { get; set; }
        public string Login { get; set; }
    }
}
