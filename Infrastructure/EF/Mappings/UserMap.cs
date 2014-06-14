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
            Property(x => x.Login);
            Property(x => x.ProfileId);
            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.FirstNameEng);
            Property(x => x.LastNameEng);
            Property(x => x.Position);
            Property(x => x.Room);
            Property(x => x.DeptId);
            Property(x => x.Image);
            Property(x => x.IsEnabled);

            HasKey(x => x.Id);
        }
    }
}