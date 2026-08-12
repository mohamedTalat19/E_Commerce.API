using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.ProductDTOs;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<Result<IReadOnlyList<BrandDTO>>> GetAllBrandsAsync(CancellationToken ct)
        {
            var data = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);
            var mappedData = _mapper.Map<IReadOnlyList<BrandDTO>>(data);
            return Result<IReadOnlyList<BrandDTO>>.Ok(mappedData);
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductAsync(ProductQueryParams request,CancellationToken ct)
        {
            var spec = new ProductWithTypeAndBrandSpecification(request);
             var data = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec,ct);
            var mappedData = _mapper.Map<IReadOnlyList<ProductDto>>(data);
            var countSpec = new ProductCountSpecification(request);
            var count = await _unitOfWork.GetRepository<Product, int>().GetCountAsync(countSpec, ct);
            var result = new PaginatedResult<ProductDto>(request.PageNumber, request.PageSize, count, mappedData);
            return Result<PaginatedResult<ProductDto>>.Ok(result);
        }

        public async Task<Result<IReadOnlyList<TypeDTO>>> GetAllTypesAsync(CancellationToken ct)
        {
            var data = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct);
            var mappedData = _mapper.Map<IReadOnlyList<TypeDTO>>(data);
            return Result<IReadOnlyList<TypeDTO>>.Ok(mappedData);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var data = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec, ct);
            if (data == null)
                return new Error("Product.NotFound", $"The Product With Id {id} Was Not Found");
            var mappedData = _mapper.Map<ProductDto>(data);
            return mappedData;
        }
    }
}
