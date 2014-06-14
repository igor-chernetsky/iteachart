using System.Data.Entity.ModelConfiguration;
using Infrastructure.EF.Domain;

namespace Infrastructure.EF.Mappings
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("T_Category");
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.ParentId).IsOptional();

            HasOptional(x => x.ParentCategory)
                .WithMany(x => x.ChildCategories)
                .HasForeignKey(x => x.ParentId);

            HasKey(x => x.Id);
        }
    }
}
