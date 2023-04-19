using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Domain.SeedWork
{
    public interface IGenericRepository<T> where T : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> AddAsync(T entity);
        Task<T[]> AddRangeAsync(T[] entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, string include = ""); //use specification later 
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int Id, params string[] includes);
        Task<T> Get(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task UpdateAsync(T entity);
    }
}
