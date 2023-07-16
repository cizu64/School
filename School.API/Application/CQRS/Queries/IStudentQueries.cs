using School.Domain.Aggregates.StudentAggregate;

namespace School.API.Application.CQRS.Queries
{
    public interface IStudentQueries
    {
        Task<Student> GetStudentById(int studentId);
        Task<IReadOnlyCollection<StudentCourse>> GetstudentCourses(int studentId);

    }
}