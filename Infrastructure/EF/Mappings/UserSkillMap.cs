using System.Data.Entity.ModelConfiguration;
using Infrastructure.EF.Domain;

namespace Infrastructure.EF.Mappings
{
    public class UserSkillMap : EntityTypeConfiguration<UserSkill>
    {
        public UserSkillMap()
        {
            ToTable("T_User_Skill");
            
            HasKey(x => x.Id);

            HasRequired(x => x.User)
                .WithMany(x => x.AddedSkills)
                .HasForeignKey(x => x.UserId);

            HasRequired(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);
        }
    }
}