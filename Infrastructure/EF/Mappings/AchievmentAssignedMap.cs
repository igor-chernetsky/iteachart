using System.Data.Entity.ModelConfiguration;
using Infrastructure.EF.Domain;

namespace Infrastructure.EF.Mappings
{
    public class AchievmentAssignedMap:EntityTypeConfiguration<AchievmentAssigned>
    {
        public AchievmentAssignedMap()
        {
            ToTable("T_Achievment_Assigned");
            HasKey(x => x.Id);

            HasRequired(x => x.Achievment)
                .WithMany(x => x.UsersAssigned)
                .HasForeignKey(x => x.AchievmentId);

            HasRequired(x => x.User)
                .WithMany(x => x.AchievmentsAssigned)
                .HasForeignKey(x => x.UserId);
        }
    }
}