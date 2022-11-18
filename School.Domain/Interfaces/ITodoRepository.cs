using School.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Interfaces
{
    public interface ITodoRepository:IGenericRepository<Todo>
    {
        Task<Todo> AddTodo(int studentId, string name, string desc);
        Task<IReadOnlyList<Todo>> GetTodos(int studentId);
    }
}
