using Infrastructure.EF.Domain;
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
        Dictionary<User, double> GetUsersTop();
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


        public Dictionary<User, double> GetUsersTop()
        {
            Dictionary<User, double> dictResult = new Dictionary<User, double>();
            var tt = attemptRepo.Query().GroupBy(a => a.User, a => a.Score, 
                (user, score) => new { user = user, score = score.Average() }).OrderByDescending(a=>a.score);

            foreach (var t in tt)
            {
                dictResult.Add(t.user, t.score);
            }
            return dictResult;
        }
    }
}
