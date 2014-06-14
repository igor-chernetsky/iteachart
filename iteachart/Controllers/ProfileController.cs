using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;

namespace iteachart.Controllers
{
    [Authorize]
    public class ProfileController : BaseSecureController
    {
        private IUserService userService;
        private ICategoryService categoryService;
        public ProfileController(IUserService userService, ICategoryService categoryService)
        {
            this.userService = userService;
            this.categoryService = categoryService;
        }

        // GET: /Profile/1
        public ActionResult My()
        {
            var userInfo = FIllUserInfo(CurrentUser.Id);
            return View("Index", userInfo);
        }

        [AllowAnonymous]
        public ActionResult Index(int id)
        {
            var userInfo = FIllUserInfo(id);
            return View(userInfo);
        }

        private UserProfileModel FIllUserInfo(int id)
        {
            var userInfo = userService.GetUserInfo(id);
            userInfo.Skills = userService.GetUserSkills(id);
            userInfo.CanEdit = CurrentUser!=null && id == CurrentUser.Id;
            var skillIds = userInfo.Skills.SelectMany(x => x.SubSkills.Select(s => s.SkillId));
            ViewBag.Categories = categoryService.GetCategoriesByParent(null).Select(x => new IdNameParentModel
            {
                Id = x.Id,
                Name = x.Name,
                Childs = x.ChildCategories.Where(a => !skillIds.Contains(a.Id)).Select(c => new IdNameModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            }).ToList();
            return userInfo;
        }

        public ActionResult AddSkill(int categoryId)
        {
            if (ModelState.IsValid)
            {
                userService.AddSkill(new AddSkillModel
                {
                    UserId = CurrentUser.Id,
                    CategoryId = categoryId
                });
                return RedirectToAction("My");
            }
            var user = FIllUserInfo(CurrentUser.Id);
            return View("Index", user);

        }

        public ActionResult RemoveSkill(int categoryId)
        {
            userService.RemoveSkill(categoryId, CurrentUser.Id);
            return RedirectToAction("My");
        }
    }
}