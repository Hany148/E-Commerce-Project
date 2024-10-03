using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts___Interface__
{
    public abstract class Specification<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; }
        public List<Expression<Func<T, object>>> IncludedExpression { get; } = new();

        protected Specification (Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;

        }

        protected void AddInclude(Expression<Func<T, object>> Include)
        {
            IncludedExpression.Add(Include);
        }

    }
}
