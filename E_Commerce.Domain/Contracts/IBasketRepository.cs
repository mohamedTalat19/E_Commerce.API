using E_Commerce.Domain.Entities.BasketEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        //Get
        Task<CustomerBasket> GetBasketAsync(string basketId, CancellationToken ct = default);
       
        //Cerate Or Update
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = default, CancellationToken ct = default);

        //Delete
        Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default);
    }
}
