using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;

namespace iteachart.Controllers
{
    public class ProfileController : Controller
    {
        private IUserService userService;
        private ICategoryService categoryService;
        public ProfileController(IUserService userService, ICategoryService categoryService)
        {
            this.userService = userService;
            this.categoryService = categoryService;
        }

        // GET: /Profile/
        public ActionResult Index(int id)
        {
            var userInfo = FIllUserInfo(id);
            return View(userInfo);
        }

        private UserProfileModel FIllUserInfo(int id)
        {
            var userInfo = userService.GetUserInfo(id);
            userInfo.Skills = userService.GetUserSkills(id);
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

        public ActionResult AddSkill(AddSkillModel model)
        {
            if (ModelState.IsValid)
            {
                userService.AddSkill(model);
                return RedirectToAction("Index", new { id = model.UserId });
            }
            var user = FIllUserInfo(model.UserId);
            return View("Index", user);

        }
    }
}