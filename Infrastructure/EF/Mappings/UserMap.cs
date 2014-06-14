using System.Data.Entity.ModelConfiguration;
using Infrastructure.EF.Domain;

namespace Infrastructure.EF.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("T_User");
            Property(x => x.Id).IsRequired();
            Property(x => x.Login).IsRequired();

            HasKey(x => x.Id);
        }

        public CategoryMap()
        {
            ToTable("T_Category");
            Property(x=>x.Id).IsRequired();
            Property(x=>x.Name).IsRequired();
            Property(x=>x.ParentId);

            HasKey(x=>x.Id);
        }
    }
}