using MediatR;
using School.Domain.SeedWork;

namespace School.API.Application.CQRS.Commands
{
    public class EnrollCourseCommandHandler : IRequestHandler<EnrollCourseCommand, bool>
    {
        private readonly IStudentService _studentService;
        public EnrollCourseCommandHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        /// <summary>
        /// Handler that processes the command when student enroll for a course
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
        {
            await _studentService.EnrollCourse(request.StudentId, request.courseId);
            return true; 
        }
    }
}
