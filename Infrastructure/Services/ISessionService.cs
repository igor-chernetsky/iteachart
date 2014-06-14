using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.Code;
using Infrastructure.Exceptions;
using Infrastructure.Models;
using Infrastructure.Secure;
using StructureMap.Building;

namespace Infrastructure.Services
{
    public interface ISessionService
    {
        void Login(LoginModel model);

        void LogOut();

        bool IsLoggedIn();
    }

    public class SessionService : ISessionService
    {
        readonly ISessionFactory sessionFactory;

        public SessionService(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public void Login(LoginModel model)
        {
            var token = sessionFactory.GetSession(model.UserName, model.Password);

            var session = new SessionUser
            {
                AuthorizationToken = token,
                UserName = model.UserName
            };

            HttpContext.Current.Session[Constants.UserKey] = session;
        }

        public void LogOut()
        {
            HttpContext.Current.Session[Constants.UserKey] = null;
        }

        public bool IsLoggedIn()
        {
            return HttpContext.Current.Session[Constants.UserKey] != null;
        }
    }
}
