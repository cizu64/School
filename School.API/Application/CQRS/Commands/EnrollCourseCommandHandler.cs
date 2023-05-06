using MediatR;
using School.Domain.Aggregates.StudentAggregate;
using School.Domain.SeedWork;

namespace School.API.Application.CQRS.Commands
{
    public class EnrollCourseCommandHandler : IRequestHandler<EnrollCourseCommand, bool>
    {
        private readonly IGenericRepository<Student> _studentRepository;
        public EnrollCourseCommandHandler(IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }
        /// <summary>
        /// Handler that processes the command when student enroll for a course
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId, "StudentCourses");
            student.EnrollCourse(student.Id, request.courseId);
            await _studentRepository.UpdateAsync(student);
            await _studentRepository.UnitOfWork.SaveAsync();
            return true;
        }
    }
}
