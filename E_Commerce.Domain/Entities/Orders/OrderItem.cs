using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrderd Product {  get; set; }
        public decimal Price { get; set; } = default!;
        public int Quantity { get; set; } = default!;


    }
}
