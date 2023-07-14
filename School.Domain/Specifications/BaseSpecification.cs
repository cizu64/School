using School.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : Entity
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria) 
        {
            Criteria = criteria;
        }
       
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes => new();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression); 
        }   
       
    }
}
