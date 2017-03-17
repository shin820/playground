using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Lux.Authorization;

namespace Lux
{
    [DependsOn(
        typeof(LuxCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class LuxApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<LuxAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}