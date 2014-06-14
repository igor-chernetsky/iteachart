using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.EF.Domain;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        UserProfileModel GetUserInfo(int userId);
        List<UserProfileSkill> GetUserSkills(int userId);

        IEnumerable<User> GetUsersList();
    }

    public class UserService : IUserService
    {
        private IRepository<User> userRepo;
        private IRepository<UserSkill> userSkillRepo;

        public UserService(IRepository<User> userRepo)
        {
            this.userRepo = userRepo;
        }

        public UserProfileModel GetUserInfo(int id)
        {
            var user = userRepo.Get(id);
            if (user == null)
            {
                throw new Exception("User was not found");
            }
            var model = new UserProfileModel
            {
                //todo add real data from user object
                UserName = user.FirstNameEng,
                UserLastName = user.LastNameEng,
                Age = 26,
                Position = user.Position
            };
            return model;
        }

        public List<UserProfileSkill> GetUserSkills(int userId)
        {
            var userSkills = (from skill in userSkillRepo.Query()
                             where skill.UserId == userId
                             group skill by skill.Category.ParentCategory into skillsGrouped
                             select new UserProfileSkill
                             {
                                 SkillName = skillsGrouped.Key.Name,
                                 SubSkills = skillsGrouped.Select(x=>new UserProfileSubSkill
                                 {
                                     SkillName = x.Category.Name,
                                     IsApproved = x.IsApproved
                                 })
                             })
                             .ToList();
            return userSkills;
        }


        public IEnumerable<User> GetUsersList()
        {
            IEnumerable<User> result = userRepo.Query();
            return result;
        }
    }
}