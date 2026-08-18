using E_Commerce.Domain.Entities.BasketEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.BasketDTOs
{
    public class BasketDTO
    {
        public string Id { get; set; }
        public ICollection<BasketItem> Items { get; set; }
    }
}
