using FrontEnd.Models;
using Infraestructure.ContextDB;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Buscar por correo y password
            var usuario = _context.DimUsuarios
                .FirstOrDefault(u =>
                    u.Correo == model.Correo &&
                    u.Password == model.Password);

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View(model);
            }

            // Guardar sesión
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetString("UsuarioCorreo", usuario.Correo);

            return RedirectToAction("Index", "Home");
        }
    }
}
