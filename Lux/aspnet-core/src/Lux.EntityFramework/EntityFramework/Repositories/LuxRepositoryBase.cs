using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Lux.EntityFramework.Repositories
{
    public abstract class LuxRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<LuxDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected LuxRepositoryBase(IDbContextProvider<LuxDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class LuxRepositoryBase<TEntity> : LuxRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected LuxRepositoryBase(IDbContextProvider<LuxDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
