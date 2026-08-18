using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.BasketDTOs;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<Result<BasketDTO>> CreateOrUpdateBasketAsync(BasketDTO basket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var mappedBasket = _mapper.Map<CustomerBasket>(basket);
            var result = await _basketRepository.CreateOrUpdateBasketAsync(mappedBasket, timeToLive, ct);

            return result == null ? Result<BasketDTO>.Fail(Error.Failure("Basket.CreationFailed", "Couldn't Create Basket"
                )) : Result<BasketDTO>.Ok(basket);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
            var result = await _basketRepository.DeleteBasketAsync(basketId, ct);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("Basket.notFound", "Couldn't Delete Basket"));
        }

        public async Task<Result<BasketDTO>> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
            var result = await _basketRepository.GetBasketAsync(basketId, ct);
            return result == null ? Result<BasketDTO>.Fail(Error.NotFound("Basket.notFound", "Couldn't Delete Basket"))
                : Result<BasketDTO>.Ok(_mapper.Map<BasketDTO>(result));
        }
    }
}
