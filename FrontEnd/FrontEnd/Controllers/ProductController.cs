using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 10)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var model = new ProductListFilterViewModel
            {
                Search = search
            };

            try
            {
                var response = await _productService.ProductListAsync(search, page, pageSize);

                if (response != null && response.Success && response.Data != null)
                {
                    model.Products = response.Data;
                }
                else
                {
                    model.ErrorMessage = response?.Message ?? "No se pudieron cargar los productos.";
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Error al obtener productos: {ex.Message}";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreatePartial()
        {
            return PartialView("Partial/_CreatePartial");
        }



        [HttpGet]
        public async Task<IActionResult> ProductTablePartial(string? search, int page = 1, int pageSize = 10)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var model = new ProductListFilterViewModel { Search = search };

            var response = await _productService.ProductListAsync(search, page, pageSize);
            if (response != null && response.Success && response.Data != null)
                model.Products = response.Data;

            return PartialView("Partial/_ProductTable", model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateEditViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("Partial/_CreatePartial", model);

            var response = await _productService.CreateAsync(model);

            if (response != null && response.Success)
                return Ok();

            ModelState.AddModelError("", response?.Message ?? "Error al crear el producto");
            return PartialView("Partial/_CreatePartial", model);
        }




        [HttpGet]
        public async Task<IActionResult> UpdatePartial(int id)
        {
            var response = await _productService.GetByIdAsync(id);

            if (response == null || !response.Success || response.Data == null)
                return BadRequest("No se encontró el producto");

            var model = new ProductCreateEditViewModel
            {
                IdProduct = response.Data.IdProduct,
                Code = response.Data.Code,
                Name = response.Data.Name,
                Price = response.Data.Price,
                Quantity = response.Data.Quantity
            };

            return PartialView("Partial/_UpdatePartial", model);
        }



        public async Task<IActionResult> Update(ProductCreateEditViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _productService.UpdateAsync(model);

            if (response != null && response.Success)
                return Ok();

            return BadRequest(response?.Message ?? "Error al actualizar el producto");
        }

    }
}
