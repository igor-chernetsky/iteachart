using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Domain
{
    public class Category
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int? ParentId { get; set; }
        public virtual Category ParentCategory { get; set; }

        /// <summary>
        /// Property for top level category
        /// </summary>
        public virtual ICollection<Category> ChildCategories { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
