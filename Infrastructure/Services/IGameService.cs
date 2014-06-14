using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Code;
using Infrastructure.EF.Domain;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public interface IGameService
    {
        UserProfileModel GetRandomUser(int id);
        IEnumerable<UserProfileModel> GetRandomUsers(int id);
    }

    public class GameService : IGameService
    {
        private IRepository<User> userRepo;
        private IUserService userService;
        public GameService(IRepository<User> userRepo, IUserService userService)
        {
            this.userRepo = userRepo;
            this.userService = userService;
        }

        public UserProfileModel GetRandomUser(int id)
        {
            var userIds = (from u in userRepo.Query()
                           where u.Id != id && !u.PlayedUsers.Select(x => x.Id).Contains(id)
                           select u.Id).ToList();
            var random = new Random().Next(userIds.Count());
            var userId = userIds[random];
            var user = userService.GetUserInfo(userId);
            return user;
        }

        public IEnumerable<UserProfileModel> GetRandomUsers(int id)
        {
            var userIds = (from u in userRepo.Query()
                           where u.Id != id && !u.PlayedUsers.Select(x => x.Id).Contains(id)
                           select u.Id).ToList();
            var randomIds = new List<int>(Constants.RandomUserNum);
            var randomizer = new Random();
            while (randomIds.Count < Constants.RandomUserNum)
            {
                var idx = randomizer.Next(0, userIds.Count);
                var user = userIds[idx];
                if (!randomIds.Contains(user))
                {
                    randomIds.Add(user);
                }
            }
            return randomIds.Select(randomId => userService.GetUserInfo(randomId));
        }
    }
}