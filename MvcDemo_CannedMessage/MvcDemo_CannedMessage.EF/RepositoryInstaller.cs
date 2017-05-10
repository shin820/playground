using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo_CannedMessage.Repository
{
    public static class RepositoryInstaller
    {
        public static void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<RepositoryFacility>();
        }
    }
}
