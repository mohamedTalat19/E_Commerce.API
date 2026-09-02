using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.Orders
{
    //Table
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, OrderAddress shipToAddress, ICollection<OrderItem> items, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            Items = items;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string PaymentIntentId { get; set; }
        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status  { get; set; } = OrderStatus.Pending;
        public OrderAddress ShipToAddress { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public int DeliveryMethodId { get; set; }    //FK
        public decimal GetTotal() => SubTotal + (DeliveryMethod?.Cost ?? 0);
        





    }
}
