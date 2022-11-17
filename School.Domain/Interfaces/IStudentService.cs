using System.Threading.Tasks;

namespace School.Domain.Interfaces
{
    public interface IStudentService
    {
        Task EnrollCourse(int studentId, int courseId);
    }
}