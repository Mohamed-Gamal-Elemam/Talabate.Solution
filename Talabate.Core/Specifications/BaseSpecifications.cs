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
        public Expression<Func<T, object>> orderBy { get ; set ; } = null;
        public Expression<Func<T, object>> orderByDesc { get ; set ; } = null;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 0;
        public bool IsPaginationEnable { get ; set ; }

        public BaseSpecifications()
        {
        
        }
        public BaseSpecifications(Expression<Func<T, bool>>? Criteriaexpression)
        {
            Criteria = Criteriaexpression;
            
        }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            orderBy=orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            orderByDesc = orderByDescExpression;
        }

        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;
        }
    }
}
