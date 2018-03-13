using TcmHMS.EntityFramework;
using EntityFramework.DynamicFilters;

namespace TcmHMS.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly TcmHMSDbContext _context;

        public InitialHostDbBuilder(TcmHMSDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
