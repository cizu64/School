using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Entities
{
    public class Course : BaseEntity, IAggregateRoot
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
