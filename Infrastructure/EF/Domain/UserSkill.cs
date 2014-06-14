namespace Infrastructure.EF.Domain
{
    public class UserSkill
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        /// <summary>
        /// Should be 2 level category
        /// </summary>
        public int CategoryId { get; set; }

        public bool IsApproved { get; set; }

        public virtual User User { get; set; }

        public virtual Category Category { get; set; }

    }
}