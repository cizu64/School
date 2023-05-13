using School.Domain.SeedWork;

namespace School.Domain.Aggregates
{
    public class Department : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; } = true;

        public Department(string name, bool isActive=true)
        {
            IsActive = isActive;
            Name = name;
        }
    }
}
