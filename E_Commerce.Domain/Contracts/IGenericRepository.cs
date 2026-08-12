using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>, new()
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct);

        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct);
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct);
        Task<int> GetCountAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct);



    }
}
