using System.Data.Entity.ModelConfiguration;
using Infrastructure.EF.Domain;

namespace Infrastructure.EF.Mappings
{
    public class AchievmentMap:EntityTypeConfiguration<Achievment>
    {
        public AchievmentMap()
        {
            ToTable("T_Achievment");
            HasKey(x => x.Id);
        }
    }
}