using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TurnosMedicos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TurnosMedicos.Controllers
{
    public class LoginController : Controller
    {
        private readonly TurnosContext _context;

        public LoginController(TurnosContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Usuario _usuario)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == _usuario.Email && u.Password == _usuario.Password);

            if (usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));
                identity.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol));
                identity.AddClaim(new Claim("Password", usuario.Password));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Mensaje = "La contraseña no es válida.";
            return View();
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Nombre,Email,Password,Rol")] Usuario usuario)
        {
            if (_context.Usuario.Any(u => u.Email == usuario.Email))
                ModelState.AddModelError("Email", "Ya existe un usuario registrado con ese Email");

            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
    }
}
