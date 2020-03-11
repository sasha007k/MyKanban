using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntityBase
    {
        public readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public virtual Task AddAsync(TEntity entity)
        {
            return _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual Task<TEntity> FindByIdAsync(TKey id)
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public virtual void RemoveById(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
