using System.Linq;

namespace Infrastructure.Repositories
{
    public class CrudRepository<T> : IRepository<T> where T : class
    {
        public CrudRepository()
        {
            
        }

        public void Add(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> Query()
        {
            throw new System.NotImplementedException();
        }
    }
}