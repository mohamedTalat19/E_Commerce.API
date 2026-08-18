using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.BasketDTOs
{
    public class BasketItemDTO
    {
        [Required(ErrorMessage = "You Must Enter The Basket Item Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You Must Enter The Basket Item Name")]
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        [Range(1, 100000)]
        public decimal Price { get; set; }
        [Range(1, 50)]
        public int Quantity { get; set; }
    }
}

