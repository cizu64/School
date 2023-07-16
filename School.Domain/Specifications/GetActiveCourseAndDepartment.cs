using School.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Specifications
{
    public class GetActiveCourseAndDepartment : BaseSpecification<Course>
    {
        public GetActiveCourseAndDepartment() : base(c => c.IsActive) //the criteria passed in the base parameter
        {
            AddInclude(c => c.Department); //including deparment entity
        }
    }
}
