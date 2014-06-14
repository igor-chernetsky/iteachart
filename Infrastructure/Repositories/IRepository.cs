using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        IQueryable<T> Query();
    }
}