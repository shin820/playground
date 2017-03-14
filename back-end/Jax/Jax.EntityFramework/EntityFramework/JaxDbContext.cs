using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using Jax.Authorization.Roles;
using Jax.Entities;
using Jax.MultiTenancy;
using Jax.Users;

namespace Jax.EntityFramework
{
    public class JaxDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        public DbSet<Task> Tasks { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public JaxDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in JaxDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of JaxDbContext since ABP automatically handles it.
         */
        public JaxDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public JaxDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
