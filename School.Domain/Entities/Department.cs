using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Entities
{
    public class Department : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
