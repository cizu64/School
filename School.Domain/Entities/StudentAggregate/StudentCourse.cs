using School.Domain.SeedWork;

namespace School.Domain.Aggregates.StudentAggregate
{
    public class StudentCourse : Entity
    {
        public int StudentId { get; private set; }
        public int CourseId { get; private set; }

        public StudentCourse(int courseId, int studentId)
        {
            CourseId = courseId;
            StudentId = studentId;
        }
    }
}
