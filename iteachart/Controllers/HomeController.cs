using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Services;
using Infrastructure.EF.Domain;
using iteachart.Models;

namespace iteachart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private ISessionService sessionService;
        private ICategoryService categoryService;

        public HomeController(IUserService userService, ISessionService sessionService, ICategoryService categoryService)
        {
            this.userService = userService;
            this.sessionService = sessionService;
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
			if (!sessionService.IsLoggedIn() && Request.IsAuthenticated)
            {
                return RedirectToAction("Logoff", "Account");
            }
            ViewBag.FirstLogin = TempData["FirstLogin"] != null;
            ViewBag.Deps = userService.GetDepartments();
            ViewBag.Technologies = categoryService.GetCategories().Where(x => x.ParentId != null).Select(x=>new IdNameModel
            {
                Id = x.Id,
                Name = x.Name
            });
            var users = userService.GetUsersList()
                .ToList()
                .Select(s => new UserModel
            {
                ProfileId = s.ProfileId,
                FirstName = s.FirstName,
                FirstNameEng = s.FirstNameEng,
                LastName = s.LastName,
                LastNameEng = s.LastNameEng,
                Id = s.Id,
                Image = s.Image,
                IsEnabled = s.IsEnabled,
                Position = s.Position,
                Room = s.Room,
                DeptId = s.DeptId,
                DeptName = s.Department.Name,
                Technologies = s.AddedSkills.Where(t => t.IsApproved).Select(ss => ss.Category.Name).ToList()
            }).ToList();
            return View(users);
        }
    }
}