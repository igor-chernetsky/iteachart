using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(object id);
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        void Remove(Expression<Func<T, bool>> filterExpr = null);
        IQueryable<T> Query();
    }
}