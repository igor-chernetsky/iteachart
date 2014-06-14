using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.EF.Domain;

namespace Infrastructure.Models.Smg
{
    public class DepartmentResponse : BaseResponse
    {
        public IList<Department> Depts { get; set; }

    }
}
