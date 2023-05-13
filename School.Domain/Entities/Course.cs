using School.Domain.SeedWork;

namespace School.Domain.Aggregates
{
    public class Course : Entity, IAggregateRoot
    {
        public int DepartmentId { get; private set; }

        public string Name { get; private set; }
        public Department Department { get; private set; }
        public bool IsActive { get; private set; } = true;


        public Course(int departmentId, string name, bool isActive=true)
        {
            DepartmentId = departmentId;
            Name = name;
            IsActive = isActive;
        }
    }
}
