using Infrastructure.EF.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Mappings
{
    public class QuestionMap : EntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            ToTable("T_Question");
            Property(x => x.Id).IsRequired();
            Property(x => x.QuestionString).IsRequired();
            Property(x => x.Type).IsRequired();
            Property(x => x.Answer).IsOptional();

            HasRequired(x => x.Category)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.CategoryId);

            HasKey(x => x.Id);
        }
    }
}
