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
        public HomeController(IUserService userService, ISessionService sessionService)
        {
            this.userService = userService;
            this.sessionService = sessionService;
        }

        public ActionResult Index()
        {
            var users = userService.GetUsersList().Select(s => new UserModel
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
                DeptId = s.DeptId
            }).ToList();
            return View(users);
        }
    }
}