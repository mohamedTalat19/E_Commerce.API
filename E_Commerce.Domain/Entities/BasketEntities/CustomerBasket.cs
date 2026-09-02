using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.BasketEntities
{
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;  //Created From Frontend Side [Guid]
        public ICollection<BasketItem> Items { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntendId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }

    }
}
