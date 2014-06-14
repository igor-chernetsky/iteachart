namespace Infrastructure.EF.Domain
{
    public class GuessedUser
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public virtual User Player { get; set; }
        public int GuessedId { get; set; }
        public virtual User Guessed { get; set; }
    }
}