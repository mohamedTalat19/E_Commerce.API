using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Data.DbContexts;
using E_Commerce.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data
{
    internal class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>, new()
        {
            var typeName = typeof(TEntity).Name;
            if(_repositories.TryGetValue(typeName, out var repository)) 
                return (IGenericRepository<TEntity, TKey>)repository;
            var repo = new GenericRepository<TEntity, TKey>(_dbContext);
            return repo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _dbContext.SaveChangesAsync(ct);
    }
}
