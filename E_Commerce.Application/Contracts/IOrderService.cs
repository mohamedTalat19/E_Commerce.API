using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Orders;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IOrderService
    {
        //Create Order
        Task<Result<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string email, CancellationToken ct = default);
        Task<Result<IReadOnlyList<OrderToReturnDTO>>> GetAllOrdersForUserAsync(string email, CancellationToken ct = default);
        Task<Result<OrderToReturnDTO>> GetOrderyIdAndEmailUserAsync(Guid id,string email, CancellationToken ct = default);
        Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodAsync(CancellationToken ct = default);
    }
}
