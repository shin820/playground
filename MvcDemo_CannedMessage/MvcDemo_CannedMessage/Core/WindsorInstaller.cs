using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MvcDemo_CannedMessage.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo_CannedMessage.Core
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RepositoryInstaller.Install(container, store);
        }
    }
}