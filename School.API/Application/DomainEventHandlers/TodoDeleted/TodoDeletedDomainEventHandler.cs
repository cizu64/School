using MediatR;
using School.Domain.Aggregates;
using School.Domain.DomainEvents;
using School.Domain.SeedWork;

namespace School.API.Application.DomainEventHandlers.TodoDeleted
{
    public class TodoDeletedDomainEventHandler : INotificationHandler<TodoDeletedDomainEvent>
    {
        private readonly IGenericRepository<Inbox> _inboxRepository;
        private readonly ILogger<TodoDeletedDomainEventHandler> _logger;
        public TodoDeletedDomainEventHandler(IGenericRepository<Inbox> inboxRepository, ILogger<TodoDeletedDomainEventHandler> logger)
        {
            _inboxRepository = inboxRepository;
            _logger = logger;
        }
        public async Task Handle(TodoDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{notification.StudentId} Deleting todo");

            await _inboxRepository.AddAsync(new Inbox(notification.StudentId, $"Todo({notification.TodoId}) deleted successfully")); //create default todos
            _logger.LogInformation($"{notification.StudentId} Course enrolled");
            //UnitOfWork SaveChanges will be commited when all domain event handlers have been processed
        }
    }
}
