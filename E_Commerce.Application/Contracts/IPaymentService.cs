using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IPaymentService
    {
        Task<Result<BasketDTO>> CreateOrUpdateIntentAsync(string basketId, CancellationToken ct = default);

        Task PaymentSucceeded(string paymentIntendId);

        Task PaymentFailed(string paymentIntendId);

    }
}
