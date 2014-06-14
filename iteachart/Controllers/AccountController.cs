using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Infrastructure.Code;
using Infrastructure.Exceptions;
using Infrastructure.Models;
using Infrastructure.Services;

namespace iteachart.Controllers
{
    public class AccountController : Controller
    {
        readonly ISessionService session;

        public AccountController(ISessionService session)
        {
            this.session = session;
        }

        public ActionResult Login(LoginModel model,string returnUrl)
        {
            try
            {
                if (session.IsLoggedIn())
                    session.LogOut();
                session.Login(model);
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            catch (AuthorizationException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Content(ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logoff()
        {
            try
            {
                session.LogOut();
                return Redirect(FormsAuthentication.LoginUrl);
            }
            catch (AuthorizationException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Content(ex.Message);
            }
        }
    }
}