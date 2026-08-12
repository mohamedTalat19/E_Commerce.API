using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IProductService
    {
        //Get Products
        Task<Result<PaginatedResult<ProductDto>>> GetAllProductAsync(ProductQueryParams request, CancellationToken ct);
  
        //Get Product
        Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct);

        //GetProductBrands
        Task<Result<IReadOnlyList<BrandDTO>>> GetAllBrandsAsync(CancellationToken ct);

        //GetProductTypes
        Task<Result<IReadOnlyList<TypeDTO>>> GetAllTypesAsync(CancellationToken ct);

    }
}
