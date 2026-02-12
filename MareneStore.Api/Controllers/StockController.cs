using MareneStore.Api.Responses;
using MareneStore.Application.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace MareneStore.Api.Controllers
{
    [RoutePrefix("api/v1/stocks")]
    public class StockController : ApiController
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> GetAllStocks([FromUri] int pageNumber = 1, [FromUri] int pageSize = 10)
        {
            var (products, totalCount) = await _stockService.GetAllAsync(pageNumber, pageSize);
            return Ok(ApiResponse.CreateSuccess(products, pageNumber, pageSize, totalCount));
        }

        [HttpDelete]
        [Route("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> RemoveByProductId(Guid productId) 
        {
            await _stockService.RemoveByProductIdAsync(productId); 
            return Content(HttpStatusCode.NoContent, ApiResponse.CreateSuccess()); 
        }
    }
}