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

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, string include)
        {
            return await _set.Include(include).AnyAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var data = await _set.FirstOrDefaultAsync(predicate);
            if (includes.Any())
            {
                data = Include(includes).FirstOrDefault();
            }
            return data;
        }

        public async Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {

            var data = predicate == null ? _set : _set.Where(predicate);
            if (includes.Any())
            {
                data = Include(includes).AsQueryable();
            }
            return await data.ToListAsync();
        }

        public IEnumerable<T> Include(params string[] includes)
        {
            IEnumerable<T> query = null;
            foreach (var include in includes)
            {
                query = _set.Include(include);
            }

            return query ?? _set;
        }


        public async Task<T> GetByIdAsync(int Id, params string[] includes)
        {
            var data = await _set.FindAsync(Id);
            if (includes.Any())
            {
                data = Include(includes).FirstOrDefault();
            }
            return data;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
