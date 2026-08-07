using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.Application.DTOs.ProductDTOs;
using E_Commerce.Domain.Entities.ProductEntities;
using Microsoft.Extensions.Options;

namespace E_Commerce.Application.Profiles
{
    internal class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IOptions<UrlSettings> _urlSettings;

        public PictureUrlResolver(IOptions<UrlSettings> urlSettings )
        {
            _urlSettings=urlSettings;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            //images/products/SlimFitJeans. jpg
            //https://localhost:7169/files/images/products/SlimFitJeans.jpg
            var baseUrl = _urlSettings.Value.BaseUrl.TrimEnd('/');
            var imagePath = source.PictureUrl.TrimStart('/');
            return $"{baseUrl}/Files/{imagePath}";
        }
    }

    public class UrlSettings
    {
        public string BaseUrl { get; set; }
    }
}