using MareneStore.Api.Requests;
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
        public async Task<IHttpActionResult> CreateProduct([FromBody] ProductRequest request)
        {
            request.Validate();

            var product = await _productService.CreateProductAsync(request.ToDto());
            return Ok(product);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetById(id);
            return Ok(product);
        }
    }

}