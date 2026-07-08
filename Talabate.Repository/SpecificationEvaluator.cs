using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity;
using Talabate.Core.Specifications;

namespace Talabate.Infrastructer
{
    internal static class SpecificationEvaluator<TEntithy> where TEntithy : BaseEntity
    {
        internal  static IQueryable<TEntithy>  GetQuery(IQueryable<TEntithy> InputQuery , ISpecification<TEntithy> spec) 
        {
            var query = InputQuery; //inputQuery=_dbcontext.set<TEntithy>();

            if(spec.Criteria != null)//p=>p.id==1
            {
                query=InputQuery.Where(spec.Criteria); //_dbcontext.set<TEntithy>().where(p=>p.id==1);
            }


            query = spec.Includes.Aggregate(query, (CurrentQuery, IncludExprestion) => CurrentQuery.Include(IncludExprestion));

            //_dbcontext.set<TEntithy>().where(p=>p.id==1).Includ(p=>p.brand)
            //_dbcontext.set<TEntithy>().where(p=>p.id==1).Includ(p=>p.brand).Includ(p=>p.category)

            return query;
        }
    }
}
