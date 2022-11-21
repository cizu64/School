using School.Domain.Entities;
using School.Domain.Entities.StudentAggregate;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Services
{
    public class TodoService: ITodoService
    {
        private readonly IGenericRepository<Todo> _todoRepository;
        public TodoService(IGenericRepository<Todo> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task CompleteTodo(int todoId,int studentId, DateTime dateCompleted)
        {
            var target = await _todoRepository.Get(s => s.Id == todoId && s.StudentId == studentId);
            //todo.CompleteTodo(dateCompleted);
            target.CompleteTodo(dateCompleted, target);
            await _todoRepository.UpdateAsync(target);
        }
    }
}
