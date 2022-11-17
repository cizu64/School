using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using School.Domain.Interfaces;
namespace School.Domain.Entities.StudentAggregate
{
    public class Student : BaseEntity, IAggregateRoot
    {
        private Student()
        {
            //Needed for EF
        }
        public Student(string firstname,string lastname, Address studentAddress, List<StudentCourse> studentCourses)
        {
            Firstname = firstname;
            Lastname = lastname;
            StudentAddress = studentAddress;
            _studentCourses = studentCourses;
        }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }

        public int Age { get; private set; }
        public string Gender { get; private set; }
        public Address StudentAddress { get; private set; }

        private readonly List<StudentCourse> _studentCourses = new List<StudentCourse>();

        public IReadOnlyCollection<StudentCourse> StudentCourses => _studentCourses.AsReadOnly();

        public void EnrollCourse(int studentId, int courseId)
        {
            if (!StudentCourses.Any(s => s.StudentId == studentId && s.CourseId == courseId))
            {
                //add
                _studentCourses.Add(new StudentCourse
                {
                    CourseId = courseId,
                    StudentId = studentId
                });
                return;
            }
            //update (do nothing)
        }

    }
}
