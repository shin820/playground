using Abp.Authorization;
using Jax.Authorization.Roles;
using Jax.MultiTenancy;
using Jax.Users;

namespace Jax.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
