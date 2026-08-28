using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Post /api/ Create Order => Order (BasketId - DeliveryMethod Id , Ship To Address) [Email]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO, CancellationToken ct)
        {
            return ToActionResult(await _orderService.CreateOrderAsync(orderDTO, GetEmailFromToken(), ct));
        }

        //Get/api/Orders/ Orders Of Users => [Email] -> Users Order For Logged In User
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetAllOrders(CancellationToken ct)
        {
            return ToActionResult(await _orderService.GetAllOrdersForUserAsync(GetEmailFromToken(), ct));
        }


        //Get/api/Orders/{id} Orders Of Users => Id + [Email] -> Users Order For Logged In User
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid id,CancellationToken ct)
        {
            return ToActionResult(await _orderService.GetOrderyIdAndEmailUserAsync(id,GetEmailFromToken(), ct));
        }

        // Get /api/Orders/Delivery Method => List Of Delivery Method
        [AllowAnonymous]
        [HttpGet("DeliveyMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethods(CancellationToken ct = default)
        {
            return ToActionResult(await _orderService.GetDeliveryMethodAsync(ct));
        }

    }
}
