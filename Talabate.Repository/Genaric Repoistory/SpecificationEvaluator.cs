using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity;
using Talabate.Core.Specifications;

namespace Talabate.Infrastructer.Genaric_Repoistory
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

            if (spec.orderBy is not null)
            {
                query= query.OrderBy(spec.orderBy);
            }
            else if(spec.orderByDesc is not null)
            {
                query=query.OrderByDescending(spec.orderByDesc);
            }


            if (spec.IsPaginationEnable)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }




            query = spec.Includes.Aggregate(query, (CurrentQuery, IncludExprestion) => CurrentQuery.Include(IncludExprestion));

            //_dbcontext.set<TEntithy>().where(p=>p.id==1).Includ(p=>p.brand)
            //_dbcontext.set<TEntithy>().where(p=>p.id==1).Includ(p=>p.brand).Includ(p=>p.category)

            return query;
        }
    }
}
