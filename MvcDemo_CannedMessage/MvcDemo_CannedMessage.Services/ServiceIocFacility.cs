using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using MvcDemo_CannedMessage.EF;
using MvcDemo_CannedMessage.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo_CannedMessage.Service
{
    public class ServiceIocFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Register(
            Classes.FromThisAssembly().Pick().If(t => t.Name.EndsWith("AppService"))
                    .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                    .WithService.DefaultInterfaces().LifestylePerWebRequest()
            );
        }
    }
}
