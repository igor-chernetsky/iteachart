using System.Data.Entity.ModelConfiguration;
using Infrastructure.EF.Domain;

namespace Infrastructure.EF.Mappings
{
    public class GuessedUserMap : EntityTypeConfiguration<GuessedUser>
    {
        public GuessedUserMap()
        {
            ToTable("T_Guessed_User");

            HasKey(x => x.Id);

            HasRequired(x => x.Player)
                .WithMany(x => x.GuessedUsers)
                .HasForeignKey(x => x.PlayerId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Guessed)
                .WithMany(x => x.PlayedUsers)
                .HasForeignKey(x => x.GuessedId)
                .WillCascadeOnDelete(false);
        }
    }
}