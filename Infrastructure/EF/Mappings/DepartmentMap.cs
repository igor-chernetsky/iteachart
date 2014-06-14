using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.EF.Domain;

namespace Infrastructure.EF.Mappings
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {

        public DepartmentMap()
        {
            ToTable("T_Department");
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.NumUsers).IsRequired();

            HasMany(x => x.Users)
                .WithRequired(x => x.Department)
                .HasForeignKey(x => x.DeptId);

            HasKey(x => x.Id);
        }
    }
}
