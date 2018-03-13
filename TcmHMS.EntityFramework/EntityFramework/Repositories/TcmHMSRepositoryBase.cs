using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace TcmHMS.EntityFramework.Repositories
{
    public abstract class TcmHMSRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<TcmHMSDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected TcmHMSRepositoryBase(IDbContextProvider<TcmHMSDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class TcmHMSRepositoryBase<TEntity> : TcmHMSRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected TcmHMSRepositoryBase(IDbContextProvider<TcmHMSDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
