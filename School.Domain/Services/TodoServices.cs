using School.Domain.Entities;
using School.Domain.Entities.StudentAggregate;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Services
{
    public class TodoServices: ITodoService
    {
        private readonly IGenericRepository<Todo> _todoRepository;
        public TodoServices(IGenericRepository<Todo> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task CompleteTodo(int studentId, DateTime dateCompleted)
        {
            var todo = await _todoRepository.Get(s=>s.StudentId==studentId);
            todo.CompleteTodo(dateCompleted);
            await _todoRepository.UpdateAsync(todo);
        }
    }
}
