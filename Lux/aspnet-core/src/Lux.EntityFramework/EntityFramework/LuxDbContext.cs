using System.Data.Common;
using System.Data.Entity;
using Abp.Notifications;
using Abp.Zero.EntityFramework;
using Microsoft.Extensions.Configuration;
using Lux.Authorization.Roles;
using Lux.Authorization.Users;
using Lux.Configuration;
using Lux.MultiTenancy;
using Lux.Web;

namespace Lux.EntityFramework
{
    [DbConfigurationType(typeof(LuxDbConfiguration))]
    public class LuxDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        /* Default constructor is needed for EF command line tool. */
        public LuxDbContext()
            : base(GetConnectionString())
        {

        }

        private static string GetConnectionString()
        {
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder()
                );

            return configuration.GetConnectionString(
                LuxConsts.ConnectionStringName
                );
        }

        /* This constructor is used by ABP to pass connection string.
         * Notice that, actually you will not directly create an instance of LuxDbContext since ABP automatically handles it.
         */
        public LuxDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /* This constructor is used in tests to pass a fake/mock connection. */
        public LuxDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }

        public LuxDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }
    }

    public class LuxDbConfiguration : DbConfiguration
    {
        public LuxDbConfiguration()
        {
            SetProviderServices(
                "System.Data.SqlClient",
                System.Data.Entity.SqlServer.SqlProviderServices.Instance
            );
        }
    }
}
