namespace Infrastructure.EF.Domain
{
    public class AchievmentAssigned
    {
        public int Id { get; set; }
        public int AchievmentId { get; set; }
        public int UserId { get; set; }
        public virtual Achievment Achievment { get; set; }
        public virtual User User { get; set; }
    }
}