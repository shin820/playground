using System.Linq;
using Lux.EntityFramework;
using Lux.MultiTenancy;

namespace Lux.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly LuxDbContext _context;

        public DefaultTenantCreator(LuxDbContext context)
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
