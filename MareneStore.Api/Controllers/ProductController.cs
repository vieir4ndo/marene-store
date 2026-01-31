using MareneStore.Api.Requests;
using MareneStore.Application.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace MareneStore.Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct(ProductRequest request)
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(string.Join("; ", validationResult.Errors));
            }

            var product = await _productService.CreateProductAsync(request.ToDto());

            return Ok(product);
        }

        // GET api/products
        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            // Implementation to retrieve all products
            return Ok();
        }
        // GET api/products/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult GetProductById(Guid id)
        {
            // Implementation to retrieve a product by its ID
            return Ok();
        }
    }
}