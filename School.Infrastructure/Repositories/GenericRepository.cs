using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using School.Domain.SeedWork;
using School.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity, IAggregateRoot
    {
        protected readonly SchoolContext _context;
        internal DbSet<T> _set;

        public IUnitOfWork UnitOfWork => _context;   

        public GenericRepository(SchoolContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }
        public IEnumerable<T> Specify(ISpecification<T> spec)
        {
            var includes = spec.Includes.Aggregate(_context.Set<T>().AsQueryable(),
 (current, include) => current.Include(include));
          
            return includes
            .Where(spec.Criteria)
            .AsEnumerable();
        }

        //specify with pagination goes here

        public async Task<T> AddAsync(T entity)
        {
             await _set.AddAsync(entity);
            return entity;
        }

        public async Task<T[]> AddRangeAsync(T[] entity)
        {
            await _set.AddRangeAsync(entity);
            return entity;
        }

        //public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate,string include)
        //{
        //    return await _set.Include(include).AnyAsync(predicate);
        //}

        public Task DeleteAsync(T entity)
        {
             _set.Remove(entity);
            return Task.CompletedTask;
        }

        //public async Task<T> Get(Expression<Func<T, bool>> predicate, params string[] includes)
        //{
        //    var data = await _set.FirstOrDefaultAsync(predicate);
        //    if (includes.Any())
        //    {
        //        data = Include(includes).FirstOrDefault();
        //    }
        //    return data;
        //}

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            var data = await _set.FirstOrDefaultAsync(predicate);
            return data;
        }

        //public async Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes)
        //{
        //    var data = predicate == null ? _set : _set.Where(predicate);
        //    if (includes.Any())
        //    {
        //        data = Include(includes).AsQueryable();
        //    }
        //    return await data.ToListAsync();
        //}

        public async Task<IReadOnlyList<T>> GetAll()
        {
            var result = await _set.ToListAsync();
            return result;
        }

        //IEnumerable<T> Include(params string[] includes)
        //{
        //    IEnumerable<T> query = null;
        //    foreach (var include in includes)
        //    {
        //        query = _set.Include(include);
        //    }
        //    return query ?? _set;
        //}

        //public async Task<T> GetByIdAsync(int Id, params string[] includes)
        //{
        //    var data = await _set.FindAsync(Id);
        //    if (includes.Any())
        //    {
        //        data = Include(includes).FirstOrDefault();
        //    }
        //    return data;
        //}
        public async Task<T> GetByIdAsync(int Id)
        {
            var data = await _set.FindAsync(Id);
            return data;
        }

        public Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
