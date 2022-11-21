using School.Domain.Entities;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Repositories
{
    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        public TodoRepository(SchoolContext context) : base(context)
        {
        }

        public async Task<Todo> AddTodo(int studentId,string name,string desc)
        {
            Todo t = new Todo(studentId, name, desc);
            await AddAsync(t);
            return t;
        }

        public async Task<IReadOnlyList<Todo>> GetTodos(int studentId)
        {
            var studentTodos = await GetAll(t => t.StudentId == studentId);
            return studentTodos;
        }

        public async Task<Todo> GetTodo(int todoId)
        {
            var todo = await Get(t => t.Id == todoId);
            return todo;
        }

    }
}
