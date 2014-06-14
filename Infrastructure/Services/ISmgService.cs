using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.Code;
using Infrastructure.EF.Domain;
using Infrastructure.Models;
using Infrastructure.Models.Smg;
using Infrastructure.Repositories;
using Infrastructure.Secure;
using ServiceStack;

namespace Infrastructure.Services
{
    public interface ISmgService
    {
        EmployeeResponse GetAllEmployees();

        EmployeeDetailsResponse GetEmployeeDetails(int profileId);

        void PopulateDataBase();
    }

    public class SmgService : ISmgService
    {
        private const string baseUrl = "https://smg.itechart-group.com/MobileServiceNew/MobileService.svc/";

        private SessionUser User { get; set; }

        private readonly IRepository<User> userRepo;

        public SmgService(IRepository<User> userRepo)
        {
            this.userRepo = userRepo;
            User = (SessionUser) HttpContext.Current.Session[Constants.UserKey];
        }

        private T Request<T, T1>(T1 r) where T1 : BaseRequest
        {
            var request =
                WebRequest.Create(baseUrl + r.ToString());

            WebResponse wr = request.GetResponse();
            Stream receiveStream = wr.GetResponseStream();
            var reader = new StreamReader(receiveStream, Encoding.UTF8);
            string content = reader.ReadToEnd();
            var result = content.FromJson<T>();
            return result;
        }

        public EmployeeResponse GetAllEmployees()
        {
            return Request<EmployeeResponse, AllEmployeesRequest>(new AllEmployeesRequest(User.AuthorizationToken));
        }

        public EmployeeDetailsResponse GetEmployeeDetails(int profileId)
        {
            return Request<EmployeeDetailsResponse, EmployeeDetailsRequest>(new EmployeeDetailsRequest(User.AuthorizationToken));
        }

        public void PopulateDataBase()
        {
            var employees = GetAllEmployees().Profiles;
            foreach (var e in employees)
            {
                var old = userRepo.Query().FirstOrDefault(t => t.ProfileId == e.ProfileId);
                if (old == null)
                {
                    var user = new User
                    {
                        DeptId = e.DeptId,
                        FirstName = e.FirstName,
                        FirstNameEng = e.FirstNameEng,
                        IsEnabled = e.IsEnabled,
                        LastName = e.LastName,
                        LastNameEng = e.LastNameEng,
                        Image = e.Image,
                        Position = e.Position,
                        ProfileId = e.ProfileId,
                        Room = e.Room
                    };

                    var d = GetEmployeeDetails(user.ProfileId).Details;
                    user.MiddleName = d.MiddleName;
                    user.Birthday = d.Birthday;
                    user.Skype = d.Skype;
                    user.Phone = d.Phone;
                    user.DomenName = d.DomenName;
                    user.Rank = d.Rank;
                    user.Room = d.Room;
                    user.Email = d.Email;
                    
                    userRepo.Add(user);

                    
                }
                else
                {
                    old.DeptId       = e.DeptId;
                    old.FirstName    = e.FirstName;
                    old.FirstNameEng = e.FirstNameEng;
                    old.IsEnabled    = e.IsEnabled;
                    old.LastName     = e.LastName;
                    old.LastNameEng  = e.LastNameEng;
                    old.Image        = e.Image;
                    old.Position     = e.Position;
                    old.ProfileId    = e.ProfileId;
                    old.Room         = e.Room;
                    var d = GetEmployeeDetails(old.ProfileId).Details;
                    old.MiddleName = d.MiddleName;
                    old.Birthday = d.Birthday;
                    old.Skype = d.Skype;
                    old.Phone = d.Phone;
                    old.DomenName = d.DomenName;
                    old.Rank = d.Rank;
                    old.Room = d.Room;
                    old.Email = d.Email;
                    userRepo.Update(old);
                }
            }
        }
    }
}
