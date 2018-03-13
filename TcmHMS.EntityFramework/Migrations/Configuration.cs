using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using TcmHMS.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace TcmHMS.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<TcmHMS.EntityFramework.TcmHMSDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TcmHMS";
        }

        protected override void Seed(TcmHMS.EntityFramework.TcmHMSDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
