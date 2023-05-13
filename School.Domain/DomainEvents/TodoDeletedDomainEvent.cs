using MediatR;

namespace School.Domain.DomainEvents
{
    public class TodoDeletedDomainEvent : INotification
    {
        public int TodoId { get; private set; }
        public int StudentId { get; private set; }
        public TodoDeletedDomainEvent(int todoId,int studentId)
        {
            TodoId = todoId;
            StudentId = studentId;
        }
    }
}
