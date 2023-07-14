using School.Domain.Aggregates;
using School.Domain.Aggregates.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Specifications
{
    public class GetStudentByIdAndReturnCourses : BaseSpecification<Student>
    {
        public GetStudentByIdAndReturnCourses(int studentId) : base(s => s.Id == studentId) //the criteria passed in the base parameter
        {
            AddInclude(c => c.StudentCourses); //including deparment entity
        }
    }
}
