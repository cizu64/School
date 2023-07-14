using School.Domain.Aggregates.StudentAggregate;
using School.Domain.SeedWork;
using School.Domain.Specifications;

namespace School.API.Application.CQRS.Queries
{
    //recommended to use dapper to query database because they are side effect free and offer good perfomance. But for now, repository are used
    public class StudentQueries: IStudentQueries
    {
        IGenericRepository<Student> _studentRepository;
        public StudentQueries(IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
           return student;
        }

        //includes can be moved to specification by using the specifaction pattern
        public async Task<IReadOnlyCollection<StudentCourse>> GetstudentCourses(int studentId)
        {
            //var student = await _studentRepository.GetByIdAsync(studentId, includes);
            var student = _studentRepository.Specify(new GetStudentByIdAndReturnCourses(studentId)).FirstOrDefault();
            return student.StudentCourses;
        }
    }
}
