using Microsoft.EntityFrameworkCore;
using FlowStock.Models;

namespace FlowStock.Data
{
    public class FlowStockDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public FlowStockDbContext(DbContextOptions<FlowStockDbContext> options) : base (options){}
    }
}