using System.Collections.Generic;
using System.Linq;
using School.Domain.DomainEvents;
using School.Domain.SeedWork;

namespace School.Domain.Aggregates.StudentAggregate
{
    public class Student : Entity, IAggregateRoot
    {
        private Student()
        {
        }
        public Student(string email, string password, string firstname, string lastname, int age, string gender, Address studentAddress, List<StudentCourse> studentCourses)
        {
            Firstname = firstname;
            Lastname = lastname;
            Age = age;
            Gender = gender;
            StudentAddress = studentAddress;
            _studentCourses = studentCourses;
            Email = email;
            Password = password;
        }
        public Student(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public int Age { get; private set; }
        public string Gender { get; private set; }
        public Address StudentAddress { get; private set; }

        //dont expose. Use readonlycollection and defined behaviors that can use collection methods, properties or extension methods
        private readonly List<StudentCourse> _studentCourses = new List<StudentCourse>();
        
        public IReadOnlyCollection<StudentCourse> StudentCourses => _studentCourses.AsReadOnly();

        public void EnrollCourse(int studentId, int courseId)
        {
            if (!StudentCourses.Any(s => s.StudentId == studentId && s.CourseId == courseId))
            {
                //add to the collection
                _studentCourses.Add(new StudentCourse( courseId, studentId));

                //add domain event
                var CourseEnrolledDomainEvent = new StudentEnrolledForCourseDomainEvent(courseId, studentId);
                AddDomainEvent(CourseEnrolledDomainEvent); //anytime a student enroll for course, a default todo should be created for task a student needs to do
                return;
            }
            //update (do nothing)
        }
        public bool HasEnrolled(int studentId, int courseId)
        {
            return StudentCourses.Any(s => s.StudentId == studentId && s.CourseId == courseId);
        }

    }
}
