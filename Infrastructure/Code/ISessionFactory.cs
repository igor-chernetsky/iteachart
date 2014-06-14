using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.Exceptions;
using Infrastructure.Models;
using ServiceStack;

namespace Infrastructure.Code
{
    public interface ISessionFactory
    {
        string GetSession(string username, string password);
    }

    public class SessionFactory : ISessionFactory
    {
        private const string baseUrl = "https://smg.itechart-group.com/MobileServiceNew/MobileService.svc";
        public string GetSession(string username, string password)
        {
            var request =
                WebRequest.Create(
                string.Format("{0}/Authenticate?username={1}&password={2}",
                baseUrl,
                username,
                password));

            WebResponse wr = request.GetResponse();
            Stream receiveStream = wr.GetResponseStream();
            var reader = new StreamReader(receiveStream, Encoding.UTF8);
            string content = reader.ReadToEnd();
            var result = content.FromJson<AuthenticationResponse>();
            if (!string.IsNullOrEmpty(result.ErrorCode))
            {
                throw new AuthorizationException(result.ErrorCode);
            }
            return result.SessionId;
        }
    }
}
