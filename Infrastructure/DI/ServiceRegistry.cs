using Infrastructure.Repositories;
using StructureMap.Configuration.DSL;

namespace Infrastructure.DI
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry()
        {
            Scan(scan =>
            {
                scan.AssemblyContainingType(typeof(IRepository<>));
                scan.WithDefaultConventions();
            });
            For(typeof(IRepository<>)).Use(typeof(CrudRepository<>));
        }
    }
}