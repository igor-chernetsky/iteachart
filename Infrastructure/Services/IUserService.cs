using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Infrastructure.Code;
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
        IEnumerable<User> GetUsersList();
        void RemoveSkill(int categoryId, int id);

        IList<IdNameModel> GetDepartments();

        void ApproveUserSkill(int skillid, int userId);
        IEnumerable<UserProfileModel> GetKnownUsers(int id);
    }

    public class UserService : IUserService
    {
        private IRepository<User> userRepo;
        private IRepository<Department> depRepo;
        private IRepository<UserSkill> userSkillRepo;
        private IAchievmentService achievmentService;

        public UserService(IRepository<User> userRepo, IRepository<UserSkill> userSkillRepo, IRepository<Department> depRepo)
        {
            this.userRepo = userRepo;
            this.userSkillRepo = userSkillRepo;
            this.depRepo = depRepo;
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
                Department = user.Department.Name,
                Age = (DateTime.Now - user.Birthday).Days / 365,
                ImageUrl = user.Image,
                Achievments = user.AchievmentsAssigned.Select(x => new AchievmentModel
                {
                    BadgeType = x.Achievment.BadgeType,
                    Description = x.Achievment.Description,
                    ImageUrl = x.Achievment.ImageUrl
                }),
                Position = user.Position
            };
            return model;
        }

        public List<UserProfileSkill> GetUserSkills(int userId)
        {
            var skillQuery = userSkillRepo.Query();
            var userSkills = (from skill in skillQuery
                              where skill.UserId == userId
                              group skill by skill.Category.ParentCategory into skillsGrouped
                              select new UserProfileSkill
                              {
                                  SkillName = skillsGrouped.Key.Name,
                                  SubSkills = skillsGrouped.Select(x => new UserProfileSubSkill
                                  {
                                      SkillId = x.CategoryId,
                                      SkillName = x.Category.Name,
                                      IsApproved = x.IsApproved,
                                      Raiting = skillQuery
                                        .Where(s => s.IsApproved && s.CategoryId == x.CategoryId)
                                        .Select(t => new RatingItem
                                        {
                                            UserId = t.UserId,
                                            Score = t.User.Attempts.Where(a => a.CategoryId == x.CategoryId).OrderBy(o => o.Score).FirstOrDefault().Score,
                                            FirstName = t.User.FirstNameEng,
                                            LastName = t.User.LastNameEng,
                                            ItsMe = t.UserId == userId
                                        })
                                        .OrderByDescending(o => o.Score)
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

        public void RemoveSkill(int categoryId, int userId)
        {
            var modelToREmove =
                userSkillRepo.Query().FirstOrDefault(x => x.UserId == userId && x.CategoryId == categoryId);
            if (modelToREmove != null)
            {
                userSkillRepo.Remove(modelToREmove);
            }
        }

        public IList<IdNameModel> GetDepartments()
        {
            return depRepo.Query().Select(s => new IdNameModel
            {
                Id = s.Id,
                Name = s.Name
            })
            .Distinct()
            .ToList();
        }

        public IEnumerable<UserProfileModel> GetKnownUsers(int id)
        {
            var query = userRepo.Get(id);
            var guessed = query.GuessedUsers;
            if (guessed != null)
            {
                foreach (var guessedUser in guessed)
                {
                    yield return GetUserInfo(guessedUser.Guessed.Id);
                }
            }
        }

        public void AddSkill(AddSkillModel model)
        {
            userSkillRepo.Add(new UserSkill
            {
                CategoryId = model.CategoryId,
                UserId = model.UserId
            });
        }


        public void ApproveUserSkill(int skillid, int userId)
        {
            UserSkill skill = userSkillRepo.Query().FirstOrDefault(s => s.CategoryId == skillid && s.UserId == userId);
            if (skill != null)
            {
                skill.IsApproved = true;
                userSkillRepo.Update(skill);
                try
                {
                    achievmentService.AssignBadgeIfPossible(userId, BadgeType.Completed5tests);
                    achievmentService.AssignBadgeIfPossible(userId, BadgeType.Completed10tests);
                    achievmentService.AssignBadgeIfPossible(userId, BadgeType.Completed15tests);
                }
                catch (Exception e)
                {
                        
                }
            }
        }
    }
}