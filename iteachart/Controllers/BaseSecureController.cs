using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Code;
using Infrastructure.Secure;
using Infrastructure.Services;
using StructureMap;

namespace iteachart.Controllers
{
    public class BaseSecureController : Controller
    {
        readonly ISessionService session;

        protected SessionUser CurrentUser { get { return (SessionUser)System.Web.HttpContext.Current.Session[Constants.UserKey]; } }

        public BaseSecureController()
        {
            this.session = ObjectFactory.GetInstance<ISessionService>();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.HasAccess = false;
            if (!session.IsLoggedIn())
            {
                filterContext.Result = RedirectToAction("Index", "Home");
            }
            var user = System.Web.HttpContext.Current.Session[Constants.UserKey];
            ViewBag.User = user;
            ViewBag.HasAccess = true;
            base.OnActionExecuting(filterContext);
        }
    }
}