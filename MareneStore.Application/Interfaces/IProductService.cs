using MareneStore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MareneStore.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductDto product);
        Task<ProductDto> GetById(Guid id);
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> UpdateProductAsync(ProductDto product);
    }
}
