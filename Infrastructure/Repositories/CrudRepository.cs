using System.Data.Entity;
using System.Linq;
using Infrastructure.EF;

namespace Infrastructure.Repositories
{
    public class CrudRepository<T> : IRepository<T> where T : class
    {
        private readonly MyDbContext context;

        protected DbSet<T> Set
        {
            get { return context.Set<T>(); }
        }

        public CrudRepository(MyDbContext context)
        {
            this.context = context;
        }

        public T Get(object id)
        {
            var item = Set.Find(id);
            return item;
        }

        public IQueryable<T> Query()
        {
            return Set.AsQueryable();
        }

        public void Add(T entity)
        {
            Set.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            var attachedEntity = Set.Attach(entity);
            context.Entry(attachedEntity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(T entity)
        {
            var attachedEntity = Set.Attach(entity);
            context.Entry(attachedEntity).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}