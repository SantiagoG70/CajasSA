using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Boxes.Backend.Data
{
    public class DataContext : IdentityDbContext<Usuario>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {     
        }

        // DbSets
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<ItemCarrito> ItemsCarrito { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Reportes> Reportess { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetalleOrdenes { get; set; } 
        public DbSet<OrdenTemporal> OrdenesTemporales { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ProductCategory> CategoriasProductos { get; set; } 
        public DbSet<ProductImage> ProductImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Cliente)
            .WithOne(c => c.Usuario)
            .HasForeignKey<Cliente>(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Cliente)
                .WithMany(cli => cli.Carritos)
                .HasForeignKey(c => c.ClienteId);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Cliente)
                .WithMany(cli => cli.Facturas)
                .HasForeignKey(f => f.ClienteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Carrito)
                .WithOne(c => c.Factura)
                .HasForeignKey<Factura>(f => f.CarritoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemCarrito>()
                .HasOne(ic => ic.Carrito)
                .WithMany(c => c.Items)
                .HasForeignKey(ic => ic.CarritoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemCarrito>()
                .HasOne(ic => ic.Producto)
                .WithMany(p => p.ItemsCarrito)
                .HasForeignKey(ic => ic.ProductoId);

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Factura)
                .WithMany(f => f.Detalles)
                .HasForeignKey(df => df.FacturaId);


            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Producto)
                .WithMany(p => p.DetallesFactura)
                .HasForeignKey(df => df.ProductoId);

            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Inventario)
                .WithMany(i => i.Productos)
                .HasForeignKey(p => p.InventarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(pr => pr.Productos)
                .HasForeignKey(p => p.ProveedorId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Inventario); //falta relacion con producto
                

        }
    }
}