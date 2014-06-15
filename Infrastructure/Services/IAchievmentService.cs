using System.Linq;
using Infrastructure.Code;
using Infrastructure.EF.Domain;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public interface IAchievmentService
    {
        void AddAllBadges();
        void AssignBadgeIfPossible(int playerId, BadgeType type);
    }

    public class AchievmentService : IAchievmentService
    {
        private IRepository<AchievmentAssigned> achievmentAssignmentRepo;
        private readonly IRepository<Achievment> achievmentRepo;
        private IRepository<User> userRepo;
        private IRepository<UserSkill> userSkillRepo;

        public AchievmentService(IRepository<AchievmentAssigned> achievmentAssignmentRepo, IRepository<User> userRepo, IRepository<Achievment> achievmentRepo)
        {
            this.achievmentAssignmentRepo = achievmentAssignmentRepo;
            this.userRepo = userRepo;
            this.achievmentRepo = achievmentRepo;
        }


        public void AddAllBadges()
        {
            achievmentRepo.Add(new Achievment
            {
                BadgeType = BadgeType.MostWellKnown,
                Description = "The most well known employee",
                ImageUrl = "~/Content/badges/badge_gold.png"
            });
            achievmentRepo.Add(new Achievment
            {
                BadgeType = BadgeType.MostSupervisory,
                Description = "The most supervisory employee",
                ImageUrl = "~/Content/badges/badge_gold.png"
            });
            achievmentRepo.Add(new Achievment
            {
                BadgeType = BadgeType.Completed5tests,
                Description = "Completed 5 trainings",
                ImageUrl = "~/Content/badges/badge_gold.png"
            });
            achievmentRepo.Add(new Achievment
            {
                BadgeType = BadgeType.Completed10tests,
                Description = "Completed 10 trainings",
                ImageUrl = "~/Content/badges/badge_gold.png"
            });
            achievmentRepo.Add(new Achievment
            {
                BadgeType = BadgeType.Completed15tests,
                Description = "Completed 15 trainings",
                ImageUrl = "~/Content/badges/badge_gold.png"
            });

        }

        public void AssignBadgeIfPossible(int userId, BadgeType type)
        {
            switch (type)
            {
                case BadgeType.MostWellKnown:
                    var mostWellKnownUser = userRepo.Query().Where(x=>x.Id == userId).OrderByDescending(x => x.PlayedUsers.Count).FirstOrDefault();
                    var hasBadge =
                        mostWellKnownUser.AchievmentsAssigned.FirstOrDefault(x => x.Achievment.BadgeType == type) != null;
                    if (mostWellKnownUser.Id != userId || !hasBadge)
                    {
                        achievmentAssignmentRepo.Remove(x => x.UserId == userId && x.Achievment.BadgeType == type);
                        achievmentAssignmentRepo.Add(new AchievmentAssigned
                        {
                            UserId = userId,
                            AchievmentId = achievmentRepo.Query().FirstOrDefault(x => x.BadgeType == type).Id
                        });
                    }
                    break;
                case BadgeType.MostSupervisory:
                    var mostSupervisory = userRepo.Query().Where(x=>x.Id == userId).OrderByDescending(x => x.GuessedUsers.Count).FirstOrDefault();
                    hasBadge =
                        mostSupervisory.AchievmentsAssigned.FirstOrDefault(x => x.Achievment.BadgeType == type) != null;
                    if (mostSupervisory.Id != userId || !hasBadge)
                    {
                        achievmentAssignmentRepo.Remove(x => x.UserId == userId && x.Achievment.BadgeType == type);
                        achievmentAssignmentRepo.Add(new AchievmentAssigned
                        {
                            UserId = userId,
                            AchievmentId = achievmentRepo.Query().FirstOrDefault(x => x.BadgeType == type).Id
                        });
                    }
                    break;
                case BadgeType.Completed5tests:
                case BadgeType.Completed10tests:
                case BadgeType.Completed15tests:
                    var skillesApproved = userSkillRepo.Query().Count(x => x.IsApproved);
                    if (skillesApproved <= 5)
                    {
                        achievmentAssignmentRepo.Remove(x => x.UserId == userId && 
                            (x.Achievment.BadgeType == BadgeType.Completed10tests ||x.Achievment.BadgeType == BadgeType.Completed15tests ));
                        achievmentAssignmentRepo.Add(new AchievmentAssigned
                        {
                            UserId = userId,
                            AchievmentId = achievmentRepo.Query().FirstOrDefault(x => x.BadgeType == BadgeType.Completed5tests).Id
                        });
                    }
                    else if (skillesApproved <= 10)
                    {
                        achievmentAssignmentRepo.Remove(x => x.UserId == userId &&
                            (x.Achievment.BadgeType == BadgeType.Completed15tests || x.Achievment.BadgeType == BadgeType.Completed5tests));
                        achievmentAssignmentRepo.Add(new AchievmentAssigned
                        {
                            UserId = userId,
                            AchievmentId = achievmentRepo.Query().FirstOrDefault(x => x.BadgeType == BadgeType.Completed10tests).Id
                        });
                    }else if (skillesApproved <= 15)
                    {
                        achievmentAssignmentRepo.Remove(x => x.UserId == userId &&
                            (x.Achievment.BadgeType == BadgeType.Completed10tests || x.Achievment.BadgeType == BadgeType.Completed5tests));
                        achievmentAssignmentRepo.Add(new AchievmentAssigned
                        {
                            UserId = userId,
                            AchievmentId = achievmentRepo.Query().FirstOrDefault(x => x.BadgeType == BadgeType.Completed15tests).Id
                        });
                    }
                    break;

            }
        }
    }
}