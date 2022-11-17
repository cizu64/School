using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Entities
{
    public class Todo : BaseEntity, IAggregateRoot
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}
