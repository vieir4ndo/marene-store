using MareneStore.Application.Dtos;
using MareneStore.Application.Interfaces;
using MareneStore.Web.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using static MareneStore.Web.Models.ProductViewModel;

namespace MareneStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int pageNumber = 1, int? pageSize = null)
        {
            if (!pageSize.HasValue)
            {
                pageSize = Session["ProductPageSize"] != null ? (int)Session["ProductPageSize"] : 10;
            }
            else
            {
                Session["ProductPageSize"] = pageSize.Value;
            }

            var (products, totalCount) = await _productService.GetAllAsync(pageNumber, pageSize.Value);

            return View(ProductViewModel.FromDto(products, pageNumber, pageSize.Value, totalCount));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var productDto = await _productService.GetByIdAsync(id);
            if (productDto == null)
            {
                return HttpNotFound();
            }

            var productViewModel = new ProductItemViewModel
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price ?? 0,
                Stock = productDto.Stock ?? 0,
                IsActive = productDto.IsActive ?? false,
                ImageUrl = productDto.ImageUrl
            };

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var productDto = new ProductDto
            {
                Id = model.Id.Value,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                IsActive = model.IsActive,
                ImageUrl = model.ImageUrl
            };

            await _productService.UpdateProductAsync(productDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductItemViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var productDto = new ProductDto
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                IsActive = model.IsActive,
                ImageUrl = model.ImageUrl
            };

            await _productService.CreateProductAsync(productDto);

            return RedirectToAction("Index");
        }
    }
}