﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class DomainService<TEntity> : IDomainService<TEntity> where TEntity : Entity
    {
        public IRepository<TEntity> Repository { get; set; }
        public IUserContext UserContext { get; set; }

        public virtual TEntity Find(int id)
        {
            return Repository.Find(id);
        }

        public virtual IQueryable<TEntity> FindAll()
        {
            return Repository.FindAll();
        }

        public virtual void Delete(int id)
        {
            TEntity entity = Repository.Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Repository.Update(entity);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            Repository.Insert(entity);
            return entity;
        }
    }
}
