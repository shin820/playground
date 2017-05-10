﻿using System.Linq;
using System.Threading.Tasks;
using MvcDemo_CannedMessage.Entity;

namespace MvcDemo_CannedMessage.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : EFEntity
    {
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        TEntity Find(int id);
        IQueryable<TEntity> FindAll();
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}