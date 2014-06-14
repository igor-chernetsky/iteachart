using System;
using System.Collections.Generic;
using Infrastructure.EF.Domain;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        UserProfileModel GetUserInfo(int userId);
        List<UserProfileSkill> GetUserSkills(int userId);
    }

    public class UserService : IUserService
    {
        private IRepository<User> userRepo;

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
            var skills = new List<UserProfileSkill>();

            skills.Add(
                    new UserProfileSkill
                    {
                        SkillName = ".NET",
                        SubSkills = new List<UserProfileSubSkill>
                        {
                            new UserProfileSubSkill
                            {
                                SkillName = "WinForms",
                                IsApproved = true
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "ASP.NET",
                                IsApproved = true
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "WCF",
                                IsApproved = false
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "WPF",
                                IsApproved = true
                            },
                    }
                    });
            skills.Add(
                    new UserProfileSkill
                    {
                        SkillName = "CSS",
                        SubSkills = new List<UserProfileSubSkill>
                        {
                            new UserProfileSubSkill
                            {
                                SkillName = "CSS3",
                                IsApproved = true
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "CSS Best practice",
                                IsApproved = true
                            }
                    }
                    });

            skills.Add(
                    new UserProfileSkill
                    {
                        SkillName = "Javascript",
                        SubSkills = new List<UserProfileSubSkill>
                        {
                            new UserProfileSubSkill
                            {
                                SkillName = "Knockout",
                                IsApproved = true
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "AngularJS",
                                IsApproved = true
                            }
                    }
                    });
            return skills;
        }
    }
}