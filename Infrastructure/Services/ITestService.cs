using Infrastructure.EF.Domain;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public interface ITestService
    {
        void Test();
    }

    public class TestService : ITestService
    {
        private IRepository<User> userRepo; 
        public TestService(IRepository<User> userRepo)
        {
            this.userRepo = userRepo;
        }

        public void Test()
        {
            userRepo.Add(new User
            {
                Login = "test!"
            });
        }
    }
}