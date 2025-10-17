using Microsoft.EntityFrameworkCore;
using Boxes.Shared.Entites;

namespace boxes.Backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Rol> Roles { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Alerta> Alertas { get; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<DetalleFactura> DetalleFacturas { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<Inventario> Inventarios { get; set; }
    public DbSet<ItemCarrito> ItemCarritos { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Reportes> Reportes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Rol>().HasIndex(c => c.Name).IsUnique();
    }
}