using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class GuessUserModel
    {
        public UserProfileModel UserToGuess { get; set; }
        public List<UserProfileModel> Choices { get; set; }
    }
}