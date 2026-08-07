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

        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
