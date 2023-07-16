using School.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Specifications
{
    public class GetStudentTodoByStudentId : BaseSpecification<Todo>
    {
        public GetStudentTodoByStudentId(int studentId) : base(s => s.StudentId == studentId) //the criteria passed in the base parameter
        {
        }
    }
}
