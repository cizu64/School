using School.Domain.SeedWork;

namespace School.Domain.Aggregates
{
    public class Course : Entity, IAggregateRoot
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public Department Department { get; set; }
    }
}
