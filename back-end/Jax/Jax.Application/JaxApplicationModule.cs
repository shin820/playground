using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace Jax
{
    [DependsOn(typeof(JaxCoreModule), typeof(AbpAutoMapperModule))]
    public class JaxApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
