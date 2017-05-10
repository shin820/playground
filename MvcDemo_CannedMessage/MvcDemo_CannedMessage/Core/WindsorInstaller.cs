using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MvcDemo_CannedMessage.Repository;

namespace MvcDemo_CannedMessage.Core
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ServiceIocInstaller.Install(container, store);
        }
    }
}