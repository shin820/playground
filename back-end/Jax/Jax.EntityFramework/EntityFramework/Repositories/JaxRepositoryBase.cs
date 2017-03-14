using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Jax.EntityFramework.Repositories
{
    public abstract class JaxRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<JaxDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected JaxRepositoryBase(IDbContextProvider<JaxDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class JaxRepositoryBase<TEntity> : JaxRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected JaxRepositoryBase(IDbContextProvider<JaxDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
