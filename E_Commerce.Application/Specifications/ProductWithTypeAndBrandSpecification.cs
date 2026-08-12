using E_Commerce.Application.Common;
using E_Commerce.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecification<Product, int >
    {
        public ProductWithTypeAndBrandSpecification(Expression<Func<Product, bool>> criteria = null) : base(criteria)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

        }

        public ProductWithTypeAndBrandSpecification(ProductQueryParams request) : base
            (p => (!request.typeId.HasValue || p.TypeId == request.typeId.Value)
                &&(!request.brandId.HasValue || p.BrandId == request.brandId.Value)
                &&(string.IsNullOrWhiteSpace(request.searchText) || p.Name.Contains(request.searchText)))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (request.sortingOptions)
            {
                case ProductSortingOptions.Name:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.Price:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }

            ApplyPagination(request.PageSize, request.PageNumber);
        }


        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
