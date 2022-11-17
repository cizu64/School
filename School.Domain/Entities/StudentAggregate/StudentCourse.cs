using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Entities.StudentAggregate
{
    public class StudentCourse : BaseEntity
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
