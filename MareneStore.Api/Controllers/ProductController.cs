using MareneStore.Api.Requests;
using MareneStore.Api.Responses;
using MareneStore.Application.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace MareneStore.Api.Controllers
{
    [RoutePrefix("api/v1/products")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> CreateProduct([FromBody] ProductRequest request)
        {
            request.ValidateCreation();

            var product = await _productService.CreateProductAsync(request.ToDto());
            return Ok(ApiResponse.CreateSuccess(product));
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllProducts([FromUri] int pageNumber = 1, [FromUri] int pageSize = 10)
        {
            var products = await _productService.GetAllAsync(pageNumber, pageSize);
            return Ok(ApiResponse.CreateSuccess(products, pageNumber, pageSize));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(ApiResponse.CreateSuccess(product));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest request)
        {
            request.ValidateUpdate();
            var productDto = request.ToDto();
            productDto.Id = id;
            var updatedProduct = await _productService.UpdateProductAsync(productDto);
            return Ok(ApiResponse.CreateSuccess(updatedProduct));
        }
    }
}