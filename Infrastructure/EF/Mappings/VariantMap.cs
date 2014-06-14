using Infrastructure.EF.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Mappings
{
    public class VariantMap : EntityTypeConfiguration<Variant>
    {
        public VariantMap()
        {
            ToTable("T_Variant");
            Property(x => x.Id).IsRequired();
            Property(x => x.Text).IsRequired();
            Property(x => x.IsCorrect).IsRequired();

            HasRequired(x => x.Question)
                .WithMany(x => x.Variants)
                .HasForeignKey(x => x.QuestionId);

            HasKey(x => x.Id);
        }
    }
}
