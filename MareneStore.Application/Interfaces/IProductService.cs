using MareneStore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MareneStore.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductDto product);
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> UpdateProductAsync(ProductDto product);
        Task<List<ProductDto>> GetActiveProductsByIdsAsync(List<Guid> ids);
        Task<List<ProductDto>> GetProductsByIdsAsync(List<Guid> ids);
    }
}
