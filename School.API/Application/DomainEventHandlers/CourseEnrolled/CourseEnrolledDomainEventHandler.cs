using MediatR;
using School.Domain.Aggregates;
using School.Domain.DomainEvents;
using School.Domain.SeedWork;

namespace School.API.Application.DomainEventHandlers.CourseEnrolled
{
    public class CourseEnrolledDomainEventHandler : INotificationHandler<StudentEnrolledForCourseDomainEvent>
    {
        private readonly IGenericRepository<Todo> _todoRepository;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly ILogger<CourseEnrolledDomainEventHandler> _logger;
        public CourseEnrolledDomainEventHandler(IGenericRepository<Todo> todoRepository, ILogger<CourseEnrolledDomainEventHandler> logger, IGenericRepository<Course> courseRepository)
        {
            _todoRepository = todoRepository;
            _logger = logger;
            _courseRepository = courseRepository;
        }
        public async Task Handle(StudentEnrolledForCourseDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{notification.StudentId} Enrolling for course");
            Course course = await _courseRepository.GetByIdAsync(notification.CourseId);
            await _todoRepository.AddAsync(new Todo(notification.StudentId, $"Course Enrolled ({course.Name})", $"You enrolled for a course {course.Name} start reading for test coming up in the next 3 days")); //create default todos
            _logger.LogInformation($"{notification.StudentId} Course enrolled");
            //UnitOfWork SaveChanges will be commited when all domain event handlers have been processed
        }
    }
}
