using AutoMapper;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Domain.Entities.Orders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly UrlSettings _settings;
        private readonly IOptions<UrlSettings> options;

        public OrderItemPictureUrlResolver(IOptions<UrlSettings> options)
        {
           _settings = options.Value;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            
            var baseUrl = _settings.BaseUrl.TrimEnd('/');
            var imagePath = source.Product.PictureUrl.TrimStart('/');
            return $"{baseUrl}/Files/{imagePath}";
        }
    }
}
