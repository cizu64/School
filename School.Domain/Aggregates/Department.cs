using School.Domain.SeedWork;

namespace School.Domain.Aggregates
{
    public class Department : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
