using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class AddSkillModel
    {
        [Required]
        public int UserId { get; set; }
 
        [Required]
        public int CategoryId { get; set; } 
    }
}