using School.Domain.Aggregates.StudentAggregate;

namespace School.API.Application.CQRS.Queries
{
    public interface IStudentQueries
    {
        Task<Student> GetStudentById(int studentId, params string[] includes);
        Task<IReadOnlyCollection<StudentCourse>> GetstudentCourses(int studentId, params string[] includes);

    }
}