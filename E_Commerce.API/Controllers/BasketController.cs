using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        //Get
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> GetBasket(string id, CancellationToken ct)
        {
            var result = await _basketService.GetBasketAsync(id, ct);
            return ToActionResult(result);
        }

        //Create or Update
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket, CancellationToken ct)
        {
            var result = await _basketService.CreateOrUpdateBasketAsync(basket, ct: ct);
            return ToActionResult(result);
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct)
        {
            var result = await _basketService.DeleteBasketAsync(id, ct);
            return ToActionResult(result);
        }
    }
}
