using System.Web.Mvc;
using Infrastructure.DI;
using StructureMap;
using StructureMap.Graph;

namespace iteachart
{
    public class DiConfig
    {
        public static void Configure()
        {
            ObjectFactory.Configure(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.AddAllTypesOf<IController>();
                });
                x.AddRegistry<CoreRegistry>();
                x.AddRegistry<ServicesRegistry>();
            });
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        } 
    }
}