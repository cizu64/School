using School.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity, IAggregateRoot
    {
        Task<T> AddAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate); //use specification later 
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(T entity);     
        Task<T> GetByIdAsync(int Id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate = null);
        Task UpdateAsync(T entity);
    }
}
