using School.Domain.SeedWork;

namespace School.Domain.Aggregates.StudentAggregate
{
    public class StudentCourse : Entity
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
