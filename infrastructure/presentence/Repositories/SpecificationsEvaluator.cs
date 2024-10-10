using Domain.Contracts___Interface__;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public static class SpecificationsEvaluator
    {
        public static IQueryable<T> GetQuery<T>
            (IQueryable<T> InputQuery, Specification<T> specification)

            where T : class
        {

            var query = InputQuery;

            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            /*
                        foreach (var item in specification.IncludedExpression)
                        {
                            query = query.Include(item);
                        }
            */

            query = specification.IncludedExpression.Aggregate(
                query
                , (currentQuery, incouldeExpression) => currentQuery.Include(incouldeExpression));


            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            else if (specification.OrderbyDec is not null)
            {
                query = query.OrderBy(specification.OrderbyDec);
            }

            if (specification.IsPaginate)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query;

        }
    }
}
