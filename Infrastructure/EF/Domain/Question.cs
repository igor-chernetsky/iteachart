using Infrastructure.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }

        public QuestionTypes Type { get; set; }
        public virtual ICollection<Variant> Variants { get; set; }

        public string Answer { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
