using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Jax.EntityFramework;

namespace Jax.Migrator
{
    [DependsOn(typeof(JaxDataModule))]
    public class JaxMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<JaxDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}