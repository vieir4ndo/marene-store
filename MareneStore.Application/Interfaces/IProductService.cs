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
        Task<(List<ProductDto>, int)> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<ProductDto> UpdateProductAsync(ProductDto product);
        Task DeleteProductAsync(Guid id);
        Task<List<ProductDto>> GetActiveProductsByIdsAsync(List<Guid> ids);
        Task<List<ProductDto>> GetProductsByIdsAsync(List<Guid> ids);
    }
}
