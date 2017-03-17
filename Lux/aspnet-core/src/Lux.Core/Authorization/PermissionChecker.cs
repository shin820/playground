using Abp.Authorization;
using Lux.Authorization.Roles;
using Lux.Authorization.Users;
using Lux.MultiTenancy;

namespace Lux.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
