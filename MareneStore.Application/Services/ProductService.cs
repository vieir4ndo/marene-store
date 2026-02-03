using MareneStore.Application.Dtos;
using MareneStore.Application.Interfaces;
using MareneStore.Domain.Entities;
using MareneStore.Domain.Exceptions;
using MareneStore.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();

            var outputProducts = new List<ProductDto>();

            foreach (var product in products)
            {
                outputProducts.Add(ProductDto.FromEntity(product));
            }

            return outputProducts;
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new NotFoundException();
            }

            return ProductDto.FromEntity(product);
        }
    }
}
