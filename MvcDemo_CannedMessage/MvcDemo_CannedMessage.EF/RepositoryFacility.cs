using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using MvcDemo_CannedMessage.EF;
using MvcDemo_CannedMessage.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo_CannedMessage.Repository
{
    public class RepositoryFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Register(Component.For<ApplicationDbContext>()
                                     .ImplementedBy<ApplicationDbContext>()
                                     .LifestylePerWebRequest(),
                            Component.For(typeof(IRepositoryBase<>))
                                     .ImplementedBy(typeof(RepositoryBase<>))
                                     .LifestylePerWebRequest(),
                                //Component.For<ICannedMessageRepository>()
                                //         .ImplementedBy<CannedMessageRepository>()
                                //         .LifestylePerWebRequest()
                                Classes.FromThisAssembly().Pick().If(t => t.Name.EndsWith("Repository"))
                                     .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                                     .WithService.DefaultInterfaces().LifestylePerWebRequest()
                                );
        }
    }
}
