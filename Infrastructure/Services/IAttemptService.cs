using Infrastructure.EF.Domain;
using Infrastructure.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IAttemptService
    {
        Attempt GetAttemptById(int id);
        void IncreaseScore(int id);
        int CreateAttempt(Attempt attempt);
        Dictionary<UserProfileModel, double> GetUsersTop();
    }

    public class AttemptService:IAttemptService
    {
        private IRepository<Attempt> attemptRepo;
        private IRepository<User> userRepo;
        public AttemptService(IRepository<Attempt> attemptRepo, IRepository<User> userRepo)
        {
            this.attemptRepo = attemptRepo;
            this.userRepo = userRepo;
        }
        public Attempt GetAttemptById(int id)
        {
            return attemptRepo.Get(id);
        }

        public void IncreaseScore(int id)
        {
            Attempt attempt = attemptRepo.Get(id);
            attempt.Score++;
            attemptRepo.Update(attempt);
        }

        public int CreateAttempt(Attempt attempt)
        {
            attemptRepo.Add(attempt);
            return attempt.Id;
        }


        public Dictionary<UserProfileModel, double> GetUsersTop()
        {
            Dictionary<UserProfileModel, double> dictResult = new Dictionary<UserProfileModel, double>();
            var tt = attemptRepo.Query().GroupBy(a => a.User, a => a.Score, 
                (user, score) => new { user = user, score = score.Average() }).OrderByDescending(a=>a.score);

            foreach (var t in tt.ToList())
            {
                UserProfileModel userModel = new UserProfileModel
                {
                    Id = t.user.Id,
                    UserName = t.user.FirstNameEng,
                    UserLastName = t.user.LastNameEng,
                    Department = t.user.Department.Name,
                    Age = (DateTime.Now - t.user.Birthday).Days / 365,
                    ImageUrl = t.user.Image,
                    Achievments = t.user.AchievmentsAssigned.Select(x => new AchievmentModel
                    {
                        BadgeType = x.Achievment.BadgeType,
                        Description = x.Achievment.Description,
                        ImageUrl = x.Achievment.ImageUrl
                    }),
                    Position = t.user.Position
                };
                dictResult.Add(userModel, t.score);
            }
            return dictResult;
        }
    }
}
