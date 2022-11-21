using School.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity, IAggregateRoot
    {
        Task<T> AddAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, string include = ""); //use specification later 
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int Id, params string[] includes);
        Task<T> Get(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task UpdateAsync(T entity);
    }
}
