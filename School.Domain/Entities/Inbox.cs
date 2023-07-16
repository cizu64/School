using School.Domain.SeedWork;
using System;

namespace School.Domain.Aggregates
{
    public class Inbox : Entity, IAggregateRoot
    {
        public int StudentId { get; private set; }
        public string Description { get; private set; }
        public DateTime? DateCreated { get; private set; } = DateTime.Now;
        private Inbox()
        {

        }
        public Inbox(int studentId, string description)
        {
            StudentId = studentId;
            Description = description;
        }
       
    }
}
