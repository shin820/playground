using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using System.Data.Entity;

namespace Social.Domain
{
    public class DomainIocInitializer
    {
        public static void Init(IKernel kernel)
        {
            kernel.Register(

                Component.For(typeof(DbContext))
                        .UsingFactoryMethod(k => { return DbContextFactory.Create(k); })
                        .LifestylePerWebRequest()
            );
        }
    }
}
