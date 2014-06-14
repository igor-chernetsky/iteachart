﻿using System;
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
        void AddSkill(AddSkillModel model);
    }

    public class UserService : IUserService
    {
        private IRepository<User> userRepo;
        private IRepository<UserSkill> userSkillRepo;

        public UserService(IRepository<User> userRepo, IRepository<UserSkill> userSkillRepo)
        {
            this.userRepo = userRepo;
            this.userSkillRepo = userSkillRepo;
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
                Id = id,
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
                                     SkillId = x.CategoryId,
                                     SkillName = x.Category.Name,
                                     IsApproved = x.IsApproved
                                 })
                             })
                             .ToList();
            return userSkills;
        }

        public void AddSkill(AddSkillModel model)
        {
            userSkillRepo.Add(new UserSkill
            {
                CategoryId = model.CategoryId,
                UserId = model.UserId
            });
        }
    }
}