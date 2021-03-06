﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Domain
{
    public class Attempt
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
