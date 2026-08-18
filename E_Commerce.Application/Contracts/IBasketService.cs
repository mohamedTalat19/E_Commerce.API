using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.BasketDTOs;
using E_Commerce.Domain.Entities.BasketEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IBasketService
    {
        //Get
        Task<Result<BasketDTO>> GetBasketAsync(string basketId, CancellationToken ct = default);

        //Cerate Or Update
        Task<Result<BasketDTO>> CreateOrUpdateBasketAsync(BasketDTO basket, TimeSpan? timeToLive = default, CancellationToken ct = default);

        //Delete
        Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default);
    }
}
