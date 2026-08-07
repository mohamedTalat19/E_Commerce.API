using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpresions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;   
        }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeExpresions.Add(include);
        }
    }

    

}
