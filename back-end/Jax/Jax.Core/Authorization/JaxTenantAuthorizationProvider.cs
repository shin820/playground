using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.MultiTenancy;
using Jax.Entities;

namespace Jax.Authorization
{
    public class JaxTenantAuthorizationProvider : AuthorizationProvider
    {
        private readonly IList<PermissionDefinition> _permissionDefinitions;

        public JaxTenantAuthorizationProvider(IList<PermissionDefinition> permissionDefinitions)
        {
            _permissionDefinitions = permissionDefinitions;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var rootPermissionDefinitions = _permissionDefinitions.Where(t => !t.ParentId.HasValue);
            foreach (var rootPermissionDefinition in rootPermissionDefinitions)
            {
                var rootPermission = context.GetPermissionOrNull(rootPermissionDefinition.Name);
                if (rootPermission == null)
                {
                    RecursiveCreatePermission(rootPermission, rootPermissionDefinition, context);
                }
            }
        }

        private void RecursiveCreatePermission(Permission permission, PermissionDefinition permissionDefinition, IPermissionDefinitionContext context)
        {
            Permission rootOrChildPermission = null;
            if (permission == null)
            {
                rootOrChildPermission = context.CreatePermission(
                   permissionDefinition.Name,
                   L(permissionDefinition.LocalizationName),
                   multiTenancySides: GetMultiTenancySides(permissionDefinition));
            }
            else
            {
                rootOrChildPermission = permission.CreateChildPermission(
                   permissionDefinition.Name,
                   L(permissionDefinition.LocalizationName),
                   multiTenancySides: GetMultiTenancySides(permissionDefinition));
            }

            var childPermissionDefinitions = _permissionDefinitions.Where(t => t.ParentId == permissionDefinition.Id).ToList();
            if (childPermissionDefinitions.Any())
            {
                foreach (var childPermissionDefinition in childPermissionDefinitions)
                {
                    RecursiveCreatePermission(rootOrChildPermission, childPermissionDefinition, context);
                }
            }
        }

        private MultiTenancySides GetMultiTenancySides(PermissionDefinition permissionDefinition)
        {
            if (permissionDefinition.IsHostSide && permissionDefinition.IsTenantSide)
            {
                return MultiTenancySides.Host | MultiTenancySides.Tenant;
            }

            if (permissionDefinition.IsHostSide)
            {
                return MultiTenancySides.Host;
            }

            return MultiTenancySides.Tenant;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, JaxConsts.LocalizationSourceName);
        }
    }
}
