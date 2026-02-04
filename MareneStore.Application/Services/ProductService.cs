using MareneStore.Application.Dtos;
using MareneStore.Application.Interfaces;
using MareneStore.Domain.Entities;
using MareneStore.Domain.Exceptions;
using MareneStore.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
                product.Price.Value,
                product.Stock.Value,
                product.IsActive.Value,
                product.ImageUrl
            );

            var createdProduct = await _productRepository.AddAsync(productEntity);

            return ProductDto.FromEntity(createdProduct);
        }

        public async Task<List<ProductDto>> GetActiveProductsByIdsAsync(List<Guid> ids)
        {
            var products = await _productRepository.FindAsync(x => x.IsActive && ids.Contains(x.Id));

            return products.ToList().ConvertAll(p => ProductDto.FromEntity(p));
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            var outputProducts = new List<ProductDto>();

            foreach (var product in products)
            {
                outputProducts.Add(ProductDto.FromEntity(product));
            }

            return outputProducts;
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new NotFoundException();
            }

            return ProductDto.FromEntity(product);
        }

        public async Task<List<ProductDto>> GetProductsByIdsAsync(List<Guid> ids)
        {
            var products = await _productRepository.FindAsync(x=> ids.Contains(x.Id));

            return products.ToList().ConvertAll(p => ProductDto.FromEntity(p));
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto product)
        {
            var productEntity = await _productRepository.GetByIdAsync(product.Id);

            if (product == null)
            {
                throw new NotFoundException();
            }

            productEntity.Name = product.Name ?? productEntity.Name;
            productEntity.Description = product.Description ?? productEntity.Description;
            if (product.Price.HasValue && product.Price >= 0)
            {
                productEntity.Price = product.Price.Value;
            }
            if (product.Stock.HasValue && product.Stock >= 0)
            {
                productEntity.Stock = product.Stock.Value;
            }
            if (product.IsActive != null)
            {
                productEntity.IsActive = product.IsActive.Value;
            }
            productEntity.ImageUrl = product.ImageUrl ?? productEntity.ImageUrl;

            _productRepository.Update(productEntity);
            await _productRepository.SaveChangesAsync();


            return ProductDto.FromEntity(productEntity);
        }
    }
}
