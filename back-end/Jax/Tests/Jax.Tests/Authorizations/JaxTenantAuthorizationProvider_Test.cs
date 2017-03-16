using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Jax.Authorization;
using Jax.Entities;
using Shouldly;
using Xunit;

namespace Jax.Tests.Authorizations
{
    public class JaxTenantAuthorizationProvider_Test : JaxTestBase
    {
        private IRepository<PermissionDefinition> _permissionDefinitionRepository;

        public JaxTenantAuthorizationProvider_Test()
        {
            _permissionDefinitionRepository = Resolve<IRepository<PermissionDefinition>>();
        }

        [Fact]
        public void Should_set_permissions()
        {
            var permissionDefinitions = new List<PermissionDefinition>
            {
                new PermissionDefinition
                {
                    Id = 1,
                    Name = "Pages",
                    IsHostSide = true,
                    IsTenantSide = true,
                    LocalizationName = "Pages"
                },
                new PermissionDefinition
                {
                    Id = 2,
                    Name = "Pages.Users",
                    IsHostSide = true,
                    IsTenantSide = true,
                    LocalizationName = "Users",
                    ParentId = 1
                },
                new PermissionDefinition
                {
                    Id = 3,
                    Name = "Pages.Tenants",
                    IsHostSide = true,
                    IsTenantSide = false,
                    LocalizationName = "Tenants",
                    ParentId = 1
                },
                new PermissionDefinition
                {
                    Id = 4,
                    Name = "Pages.Tasks",
                    IsHostSide = false,
                    IsTenantSide = true,
                    LocalizationName = "Tasks",
                    ParentId = 1
                }
            };


            var permissions = PermissionFinder.GetAllPermissions(new JaxTenantAuthorizationProvider(permissionDefinitions));

            permissions.Count.ShouldBe(4);
            permissions[0].Name.ShouldBe("Pages");
            permissions[0].Children.Count.ShouldBe(3);
        }
    }
}
