using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Services;

namespace iteachart.Controllers
{
    public class UserController : BaseSecureController
    {
        readonly ISmgService smgeService;

        public UserController(ISmgService smgeService)
        {
            this.smgeService = smgeService;
        }

        //
        // GET: /Test/
        public ActionResult Index()
        {
            var res = smgeService.GetAllEmployees();
            return View();
        }
	}
}