using MareneStore.Application.Dtos;
using MareneStore.Application.Interfaces;
using MareneStore.Domain.Entities;
using MareneStore.Infrastructure.Data.Interfaces;
using System.Threading.Tasks;

namespace MareneStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto product)
        {
            var productEntity = new Product(
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.IsActive,
                product.ImageUrl
            );

            var createdProduct = await _productRepository.AddAsync(productEntity);

            return ProductDto.FromEntity(createdProduct);
        }
    }
}
