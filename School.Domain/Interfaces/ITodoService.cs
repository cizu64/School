using System;
using System.Threading.Tasks;

namespace School.Domain.Interfaces
{
    public interface ITodoService
    {
        Task CompleteTodo(int todoId, int studentId, DateTime dateCompleted);
    }
}