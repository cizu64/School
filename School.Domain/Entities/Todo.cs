using School.Domain.SeedWork;
using System;

namespace School.Domain.Aggregates
{
    public class Todo : Entity, IAggregateRoot
    {
        public int StudentId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsCompleted { get; private set; } = false;
        public DateTime? DateCompleted { get; private set; } = null;
        private Todo()
        {

        }
        public Todo(int studentId, string name, string desc)
        {
            StudentId = studentId;
            Name = name;
            Description = desc;
        }
        public void CompleteTodo(DateTime dateCompleted, Todo target) //to complete todos
        {
            target.IsCompleted = true;
            target.DateCompleted = dateCompleted;
            target.IsActive = false;
        }
    }
}
