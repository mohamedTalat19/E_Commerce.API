using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IPaymentGateway
    {
        //Create PaymentIntend
        //amount + Currency => PaymentIntendId + ClientSecret
        Task<PaymentIntentResult> CreatePaymentIntentAsync(decimal amount, string currency, CancellationToken ct = default);

        //Update PaymentIntend
        //PaymentIntend + amount => PaymentIntend + ClientSecret
        Task<PaymentIntentResult> UpdatePaymentIntentAsync(decimal amount, string paymentIntendId, CancellationToken ct = default);
    }
}
