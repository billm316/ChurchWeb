using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext dBContext;

        public Repository(DbContext dbContext)
        {
            dBContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            dBContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dBContext.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dBContext.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(int id)
        {
            return dBContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dBContext.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            dBContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dBContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
