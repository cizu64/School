using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly SchoolContext _context;
        internal DbSet<T> _set;
        public GenericRepository(SchoolContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
             _set.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.CountAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _set.FirstOrDefaultAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                return await _set.Where(predicate).ToListAsync();
            }
            return await _set.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
           return await _set.FindAsync(Id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
