using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.Code;
using Infrastructure.Models;
using Infrastructure.Models.Smg;
using Infrastructure.Secure;
using ServiceStack;

namespace Infrastructure.Services
{
    public interface ISmgService
    {
        EmployeeResponse GetAllEmployees();
    }

    public class SmgService : ISmgService
    {
        private const string baseUrl = "https://smg.itechart-group.com/MobileServiceNew/MobileService.svc/";

        private SessionUser User { get; set; }

        public SmgService()
        {
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
    }
}
