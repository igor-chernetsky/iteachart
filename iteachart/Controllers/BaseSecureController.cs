using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Services;
using StructureMap;

namespace iteachart.Controllers
{
    public class BaseSecureController : Controller
    {
        readonly ISessionService session;

        public BaseSecureController()
        {
            this.session = ObjectFactory.GetInstance<ISessionService>();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!session.IsLoggedIn())
            {
                filterContext.Result = RedirectToAction("Index", "Home");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}