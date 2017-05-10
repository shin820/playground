using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MvcDemo_CannedMessage.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo_CannedMessage.Service
{
    public static class ServiceIocInstaller
    {
        public static void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<RepositoryIocFacility>();
            container.AddFacility<ServiceIocFacility>();
        }
    }
}
