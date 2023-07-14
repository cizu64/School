using School.Domain.Specifications;
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
        Task DeleteAsync(T entity);
        IEnumerable<T> Specify(ISpecification<T> spec);

        //Task<T> GetByIdAsync(int Id, params string[] includes);
        //Task<T> GetByIdAsync(int Id, params string[] includes);
        Task<T> GetByIdAsync(int Id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        //Task<T> Get(Expression<Func<T, bool>> predicate, params string[] includes);
        //Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task<IReadOnlyList<T>> GetAll();
        Task UpdateAsync(T entity);
    }
}
