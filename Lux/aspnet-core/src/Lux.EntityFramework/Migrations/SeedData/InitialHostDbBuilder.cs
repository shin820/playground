using Lux.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Lux.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly LuxDbContext _context;

        public InitialHostDbBuilder(LuxDbContext context)
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
