using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Specifications
{
    internal static class SpecificationEvaluator
    {
        internal static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> baseQuery,
            ISpecification<TEntity, TKey> spec) where TEntity : BaseEntity<TKey>
        {
            var query = baseQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }



            if (spec.IncludeExpresions.Any())
            {
                query = spec.IncludeExpresions.Aggregate(query, (current, nextExp) => current.Include(nextExp));
            }

            return query;
        }
    }
}
