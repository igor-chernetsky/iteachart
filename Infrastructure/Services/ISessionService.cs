using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.Code;
using Infrastructure.EF.Domain;
using Infrastructure.Exceptions;
using Infrastructure.Models;
using Infrastructure.Repositories;
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
        readonly ISmgService smgService;
        readonly IRepository<User> userRepo;

        public SessionService(
            ISessionFactory sessionFactory,
            IRepository<User> userRepo,
            ISmgService smgService)
        {
            this.sessionFactory = sessionFactory;
            this.userRepo = userRepo;
            this.smgService = smgService;
        }

        public void Login(LoginModel model)
        {
            var dbUser = new User();
            var token = "blaBla";
            try
            {

                token = sessionFactory.GetSession(model.UserName, model.Password);

                dbUser = userRepo
                    .Query()
                    .FirstOrDefault(t => t.DomenName.ToLower().Trim() == model.UserName.ToLower().Trim());
            }
            catch (Exception e)
            {
                
            }

            var session = new SessionUser
            {
                AuthorizationToken = token,
                UserName = model.UserName
            };

            if (dbUser == null)
            {
               /* smgService.PopulateDataBase(true);*/
                dbUser = userRepo.Query().FirstOrDefault();
            }

            if (dbUser != null)
            {
                session.DepId = dbUser.DeptId;
                session.DomenName = dbUser.DomenName;
                session.Email = dbUser.Email;
                session.FirstName = dbUser.FirstName;
                session.LastName = dbUser.LastName;
                session.FirstNameEng = dbUser.FirstNameEng;
                session.LastNameEng = dbUser.LastNameEng;
                session.Room = dbUser.Room;
                session.DepId = dbUser.DeptId;
                session.ImageUrl = dbUser.Image;
                session.Id = dbUser.Id;
                dbUser.ProfileId = dbUser.ProfileId;
            }
            else
            {
                session.FirstName = "Вася";
                session.LastName = "Пупкин";

            }


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
