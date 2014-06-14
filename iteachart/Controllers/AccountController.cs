using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Models;

namespace iteachart.Controllers
{
    public class AccountController : BaseSecureController
    {

        public ActionResult Login(LoginModel model)
        {
            return View();
        }
	}
}