using Microsoft.EntityFrameworkCore;
using Project.Core.Application.Interfaces;
using Project.Core.Domain.Basics;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Infrastructure.Persistence.Implementations
{
    internal abstract class Repository<TEntity> : IRepository<int, TEntity> where TEntity : BaseEntity
    {
        protected readonly DataContext context;
        public Repository(DataContext context) => this.context = context;


        // create
        public virtual async Task Add(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }
        // read
        public virtual async Task<TEntity> Get(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }
        // update
        public virtual void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
        // delete
        public virtual async Task Delete(int id)
        {
            var item = await context.Set<TEntity>().FindAsync(id);
            context.Set<TEntity>().Remove(item);
        }

        // check
        public virtual async Task<bool> CheckAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().AnyAsync(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().CountAsync(predicate);
        }
    }
}
