using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowStock.Data;
using FlowStock.Models;
using FlowStock.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace FlowStock.Controllers
{
    
    public class AccesoController : Controller
    {
        private readonly FlowStockDbContext context;

        public AccesoController(FlowStockDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult Registrate()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Registrate(UsuarioViewModel modelo)
        {
            if (modelo.Clave != modelo.ConfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            Usuario usuario = new Usuario()
            {
                NombreUsuario = modelo.NombreUsuario,
                CorreoElectronico = modelo.CorreoElectronico,
                Clave = modelo.Clave,
            };

            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();

            if (usuario.Id != 0 )
            {
                return RedirectToAction("Login","Acceso");
            }

            ViewData["Mensaje"] = "No se puede crear el usuario";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel modelo)
        {
            Usuario? usuario = await context.Usuarios.Where
            (u => u.CorreoElectronico == modelo.CorreoElectronico
            && u.Clave == modelo.Clave).FirstOrDefaultAsync();

            if(usuario == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,usuario.NombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),properties
            );
            return RedirectToAction("Registro","Producto");
        }

    }
}

