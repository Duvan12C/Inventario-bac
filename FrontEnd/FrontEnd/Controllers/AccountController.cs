using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class AccountController(AccountService accountService) : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            var token = HttpContext.Session.GetString("token");
            if (!string.IsNullOrEmpty(token))
            {
                // No hay token → redirigir a login
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await accountService.LoginAsync(model);

            if (response != null && response.Success)
            {
                HttpContext.Session.SetString("token", response.Data?.Token ?? "");
                HttpContext.Session.SetString("username", response.Data?.Nombre ?? "");

                return RedirectToAction("Index", "Home");
            }

            // Mostrar mensaje que viene del backend
            ModelState.AddModelError("", response?.Message ?? "Error al iniciar sesión");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var token = HttpContext.Session.GetString("token");
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await accountService.RegisterAsync(model);

            if (response != null && response.Success)
            {
                HttpContext.Session.SetString("token", response.Data?.Token ?? "");
                HttpContext.Session.SetString("username", response.Data?.Nombre ?? "");

                return RedirectToAction("Index", "Product");
            }

            ModelState.AddModelError("", response?.Message ?? "Error al iniciar sesión");
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
