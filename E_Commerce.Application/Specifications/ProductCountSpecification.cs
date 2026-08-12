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
    internal class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams request) :
            base
             (p => (!request.typeId.HasValue || p.TypeId == request.typeId.Value)
                &&(!request.brandId.HasValue || p.BrandId == request.brandId.Value)
                &&(string.IsNullOrWhiteSpace(request.searchText) || p.Name.Contains(request.searchText)))
        {
        }
    }
}
