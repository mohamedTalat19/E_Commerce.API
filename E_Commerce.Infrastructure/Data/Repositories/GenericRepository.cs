using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Data.DbContexts;
using E_Commerce.Infrastructure.Data.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity
        : BaseEntity<TKey>, new()
    {
        public void Add(TEntity entity)
          => _dbContext.Set<TEntity>().Add(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct)
          => await _dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct)
        {
            var baseQuery = _dbContext.Set<TEntity>();
            var fullQuery = SpecificationEvaluator.CreateQuery(baseQuery, spec);
            return await fullQuery.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct)
           => await _dbContext.Set<TEntity>().FindAsync(id, ct);

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct)
        {
            var baseQuery = _dbContext.Set<TEntity>();
            var fullQuery = SpecificationEvaluator.CreateQuery(baseQuery, spec);
            return await fullQuery.FirstOrDefaultAsync(ct);
        }

        public async Task<int> GetCountAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct)
        {
            var baseQuery = _dbContext.Set<TEntity>();
            var fullQuery = SpecificationEvaluator.CreateQuery(baseQuery, spec);
            return await fullQuery.CountAsync(ct);
        }

        public void Remove(TEntity entity)
           => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
           => _dbContext.Set<TEntity>().Update(entity);
    }
}
