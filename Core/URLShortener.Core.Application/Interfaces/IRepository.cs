using Project.Core.Domain.Basics;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Core.Application.Interfaces
{
    public interface IRepository<TKey, TEntity> where TEntity : BaseEntity
    {
        Task Add(TEntity entity);

        Task<TEntity> Get(TKey id);
        Task<IEnumerable<TEntity>> GetAll();

        void Update(TEntity entity);

        Task Delete(TKey id);

        Task<bool> CheckAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
