using System.Configuration;
using Infrastructure.Code;
using Infrastructure.EF;
using StructureMap.Configuration.DSL;
using StructureMap.Web;

namespace Infrastructure.DI
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            For<MyDbContext>().HttpContextScoped().Use(x => new MyDbContext(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString));

            For(typeof(ISessionFactory)).Use(typeof(SessionFactory));
        }
    }
}