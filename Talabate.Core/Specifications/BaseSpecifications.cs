using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Talabate.Core.Entity;

namespace Talabate.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get  ; set  ; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpecifications()
        {
        
        }
        public BaseSpecifications(Expression<Func<T, bool>>? Criteriaexpression)
        {
            Criteria = Criteriaexpression;
            
        }
    }
}
