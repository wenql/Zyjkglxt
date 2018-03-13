using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using TcmHMS.EntityFramework;

namespace TcmHMS.Migrator
{
    [DependsOn(typeof(TcmHMSDataModule))]
    public class TcmHMSMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<TcmHMSDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}