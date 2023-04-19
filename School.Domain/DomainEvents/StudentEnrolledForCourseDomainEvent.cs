using MediatR;

namespace School.Domain.DomainEvents
{
    public class StudentEnrolledForCourseDomainEvent : INotification
    {
        public int CourseId { get; private set; }
        public int StudentId { get; private set; }
        public StudentEnrolledForCourseDomainEvent(int courseId,int studentId)
        {
            CourseId = courseId;
            StudentId = studentId;
        }
    }
}
