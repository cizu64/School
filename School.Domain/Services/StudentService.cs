using School.Domain.Aggregates.StudentAggregate;
using School.Domain.SeedWork;
using System.Threading.Tasks;

namespace School.Domain.Services
{
    public class StudentService: IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;

        public StudentService(IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task EnrollCourse(int studentId, int courseId)
        {
            var stud = await _studentRepository.GetByIdAsync(studentId,"StudentCourses");
            stud.EnrollCourse(stud.Id, courseId);
            await _studentRepository.UpdateAsync(stud);
        }
      
    }
}
