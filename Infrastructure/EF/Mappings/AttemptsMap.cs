using Infrastructure.EF.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Mappings
{
    class AttemptMap: EntityTypeConfiguration<Attempt>
    {
        public AttemptMap()
        {
            ToTable("T_Attempt");
            Property(x => x.Id).IsRequired();
            Property(x => x.Score).IsRequired();

            HasRequired(x => x.Category)
                .WithMany(x => x.Attempts)
                .HasForeignKey(x => x.CategoryId);

            HasRequired(x => x.User)
                .WithMany(x => x.Attempts)
                .HasForeignKey(x => x.UserId);

            HasKey(x => x.Id);
        }
    }
}
