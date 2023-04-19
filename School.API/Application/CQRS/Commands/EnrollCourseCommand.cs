using MediatR;

namespace School.API.Application.CQRS.Commands
{
    public class EnrollCourseCommand : IRequest<bool>
    {
        public int StudentId { get; set; }
        public int courseId { get; set; }
        public EnrollCourseCommand(int studentId, int courseId)
        {
            StudentId = studentId;
            this.courseId = courseId;
        }
    }
}
