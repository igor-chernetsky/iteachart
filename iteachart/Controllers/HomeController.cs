using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Services;
using Infrastructure.EF.Domain;

namespace iteachart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            List<User> users = userService.GetUsersList().ToList();
            return View(users);
        }
    }
}