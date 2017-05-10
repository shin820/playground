using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo_CannedMessage.Core
{
    public class CastleDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public CastleDependencyResolver(IWindsorContainer container)
        {
            this._container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.ResolveAll(serviceType).Cast<object>() : new object[] { };
        }
    }
}