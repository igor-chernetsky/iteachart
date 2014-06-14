using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;

namespace iteachart.Controllers
{
    public class ProfileController : BaseSecureController
    {
        private IUserService userService;
        private ICategoryService categoryService;
        public ProfileController(IUserService userService, ICategoryService categoryService)
        {
            this.userService = userService;
            this.categoryService = categoryService;
        }

        // GET: /Profile/
        public ActionResult Index()
        {
            var userInfo = userService.GetUserInfo(1);
            userInfo.Skills = userService.GetUserSkills(1);
            return View(userInfo);
        }
    }
}