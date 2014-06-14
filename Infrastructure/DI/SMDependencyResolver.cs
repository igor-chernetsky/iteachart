using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace Infrastructure.DI
{
    public class StructureMapDependencyResolver : IDependencyResolver, IDisposable
    {
        private IContainer container;

        public StructureMapDependencyResolver(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.container = container;
        }

        public void Dispose()
        {
            if (container != null)
                container.Dispose();

            container = null;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;

            try
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? container.TryGetInstance(serviceType)
                    : container.GetInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}