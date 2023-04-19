using System.Threading.Tasks;

namespace School.Domain.SeedWork
{
    public interface IStudentService
    {
        Task EnrollCourse(int studentId, int courseId);
    }
}