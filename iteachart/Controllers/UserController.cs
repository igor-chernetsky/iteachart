using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.EF.Domain;
using Infrastructure.Repositories;
using Infrastructure.Services;
using StructureMap;

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
       /*     ObjectFactory.GetInstance<IRepository<User>>().Add(new User
                    {
                    });*/
            var res = smgeService.GetAllEmployees();
            smgeService.PopulateDataBase();
            return View();
        }
	}
}