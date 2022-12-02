using Infrastructure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Core.EntityFramework
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);
        
        Task<TEntity> GetByIdAsync(object id);
        
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync<TProperty>(Expression<Func<TEntity, TProperty>> include);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync<TProperty>(Expression<Func<TEntity, TProperty>> include, Expression<Func<TEntity, bool>> predicate);
    }
}
