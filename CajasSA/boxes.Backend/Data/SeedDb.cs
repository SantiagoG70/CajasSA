using Boxes.Shared.Entites;

namespace Boxes.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckRolesAsync();
    }

    private async Task CheckRolesAsync()
    {
        if (!_context.Roles.Any()) // Si no hay roles, los creamos
        {
            _context.Roles.Add(new Rol { Name = "Administrador" });
            _context.Roles.Add(new Rol { Name = "Cliente" });
            _context.Roles.Add(new Rol { Name = "Empleado" });
        }

        await _context.SaveChangesAsync();
    }

    private async Task CheckProductosAsync() {
        if (!_context.Productos.Any()) { 
            _context.Productos.Add(new Producto
            {
                Name = "Caja Re-armada Pequeña",
                Quantity = 3000,
                Price = 100,
                Weight = 0.02m,
                Type = "Caja Re-armada",
                EntryDate = DateTime.Now,
                Max = 45000,
                Min = 50
            });
            _context.Productos.Add(new Producto
            {
                Name = "Caja Re-armada Mediana",
                Quantity = 2000,
                Price = 150,
                Weight = 0.05m,
                Type = "Caja Re-armada",
                EntryDate = DateTime.Now,
                Max = 30000,
                Min = 30,
                
            });
            await _context.SaveChangesAsync();
        }
    }
}