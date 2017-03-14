using System.Linq;
using Jax.EntityFramework;
using Jax.MultiTenancy;

namespace Jax.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly JaxDbContext _context;

        public DefaultTenantCreator(JaxDbContext context)
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
