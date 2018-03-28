using Abp.AutoMapper;
using Abp.Modules;
using System.Reflection;
using TcmHMS.Application;

namespace TcmHMS
{
    [DependsOn(typeof(TcmHMSCoreModule), typeof(AbpAutoMapperModule))]
    public class TcmHMSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                CustomDtoMapper.CreateMappings(mapper);
            });
        }
    }
}
