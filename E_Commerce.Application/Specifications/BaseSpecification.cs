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

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagenated { get; private set; }
        protected void ApplyPagination(int pageSize, int pageNumber)
        {
            IsPagenated = true;
            Take = pageSize;
            Skip = (pageNumber - 1) * pageSize;
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> exp)
        {
            OrderBy = exp;
        }

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> exp)
        {
            OrderByDesc = exp;
        }

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
