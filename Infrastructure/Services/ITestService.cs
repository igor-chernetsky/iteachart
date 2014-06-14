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
        private IRepository<Category> categoryRepo;
        public TestService(IRepository<User> userRepo, IRepository<Category> categoryRepo)
        {
            this.userRepo = userRepo;
            this.categoryRepo = categoryRepo;
        }

        public void Test()
        {
            userRepo.Add(new User
            {
                Login = "test!"
            });

            categoryRepo.Add(new Category
            {
                Id = 1,
                Name = ".NET"
            });
        }


    }
}