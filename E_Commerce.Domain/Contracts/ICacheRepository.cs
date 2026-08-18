using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        //Get
        Task<string?> GetAsync(string cacheKey, CancellationToken ct = default);

        //Set
        Task SetAsync(string cacheKey,string cacheValue,TimeSpan? timeTolive = default ,CancellationToken ct = default);

    }
}
