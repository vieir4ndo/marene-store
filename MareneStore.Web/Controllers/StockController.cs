using MareneStore.Application.Interfaces;
using MareneStore.Web.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MareneStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int pageNumber = 1, int? pageSize = null)
        {
            if (!pageSize.HasValue)
            {
                pageSize = Session["PageSize"] != null ? (int)Session["PageSize"] : 4;
            }
            else
            {
                Session["PageSize"] = pageSize.Value;
            }

            var (stocks, totalCount) = await _stockService.GetAllAsync(pageNumber, pageSize.Value);

            return View(StockViewModel.FromDto(stocks, pageNumber, pageSize.Value, totalCount));
        }

        [HttpPost]
        public async Task<ActionResult> RemoveByProductId(Guid productId)
        {
            await _stockService.RemoveByProductIdAsync(productId);
            return RedirectToAction("Index");
        }
    }
}