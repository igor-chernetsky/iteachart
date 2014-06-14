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
    }
}