using System.Linq;
using TcmHMS.EntityFramework;
using TcmHMS.MultiTenancy;

namespace TcmHMS.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly TcmHMSDbContext _context;

        public DefaultTenantCreator(TcmHMSDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
