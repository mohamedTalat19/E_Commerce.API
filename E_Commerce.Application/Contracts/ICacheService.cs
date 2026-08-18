using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface ICacheService
    {
        //Get
        Task<string?> GetDataAsync(string cacheKey, CancellationToken ct = default);

        //Set
        Task SetDataAsync(string cacheKey, object cacheValue, TimeSpan? timeTolive = default, CancellationToken ct = default);
    }
}
