using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo_CannedMessage.Core
{
    public static class IocContainer
    {
        private static IWindsorContainer _container;

        public static void Setup()
        {
            _container = new WindsorContainer().Install(
                new ControllersInstaller(),
                new WindsorInstaller()
                );

            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            DependencyResolver.SetResolver(new CastleDependencyResolver(_container));
        }
    }
}