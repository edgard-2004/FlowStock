using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowStock.Data;
using FlowStock.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinal.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly FlowStockDbContext context;

        public ProductoController(FlowStockDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Registro(string filtroNombre = null)
        {
            var productos = context.Productos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtroNombre))
            {
                productos = productos.Where(p => p.Nombre.Contains(filtroNombre));
            }

            ViewData["FiltroNombre"] = filtroNombre;

            return View(await productos.ToListAsync());
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(Producto producto)
        {
            await context.Productos.AddAsync(producto);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Registro));
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Producto producto)
        {
            context.Productos.Update(producto);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Registro));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            context.Productos.Remove(producto);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Registro));
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Acceso");
        }
    }
}

