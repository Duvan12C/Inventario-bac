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
                // No hay token → redirigir a login
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
    }
}
