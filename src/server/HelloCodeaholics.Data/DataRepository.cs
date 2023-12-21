using HelloCodeaholics.Common.Interfaces;
using HelloCodeaholics.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HelloCodeaholics.Data
{
    public class DataRepository<T> : IGenericRepository<T> where T : class
    {
        internal HelloCodeDbContext _context;
        internal DbSet<T> _entities;
        public DataRepository(HelloCodeDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? filter = null, int pageNumber = 0, int pageSize = 0, bool asNoTracking = true)
        {
            var query = _entities.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }            
            if (pageNumber > 0 && pageSize > 0 )
            {
                query = query.Skip((pageNumber -1) * pageSize).Take(pageSize);
            }
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
            => await _entities.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null)
            => filter != null ? await _entities.AnyAsync(filter) : await _entities.AnyAsync();

        public async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<T?> UpdateAsync(T entity, params Expression<Func<T, object>>[] excludedProperties)
        {
            _context.Update(entity);
            foreach(var exclude in excludedProperties)
            {
                _context.Entry(entity).Property(exclude).IsModified = false;
            }
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }


    }
}
