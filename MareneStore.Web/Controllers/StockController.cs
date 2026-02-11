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
        public async Task<ActionResult> Index()
        {
            var stocks = await _stockService.GetAllAsync();

            return View(StockViewModel.FromDto(stocks));
        }

        [HttpPost]
        public async Task<ActionResult> RemoveByProductId(Guid productId)
        {
            await _stockService.RemoveByProductIdAsync(productId);
            return RedirectToAction("Index");
        }
    }
}