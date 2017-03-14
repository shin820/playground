using Jax.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Jax.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly JaxDbContext _context;

        public InitialHostDbBuilder(JaxDbContext context)
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
