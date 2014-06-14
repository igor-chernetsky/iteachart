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
    }

    public class AttemptService:IAttemptService
    {
        private IRepository<Attempt> attemptRepo;
        public AttemptService(IRepository<Attempt> attemptRepo)
        {
            this.attemptRepo = attemptRepo;
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
    }
}
