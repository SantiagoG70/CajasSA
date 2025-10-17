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
        await CheckProveedoresAsync();
    }

    private async Task CheckRolesAsync()
    {
        if (!_context.Roles.Any()) // Si no hay roles, los creamos
        {
            var roles = new List<Rol>
            {
                new Rol { Name = "Administrador" },
                new Rol { Name = "Cliente" },
                new Rol { Name = "Empleado" }
            };
            _context.Roles.AddRange(roles);
        }

        await _context.SaveChangesAsync();
    }

    public async Task CheckProveedoresAsync() {
        if (!_context.Proveedores.Any()) // Si no hay proveedores, los creamos
        {
            var proveedores = new List<Proveedor>
            {
                new Proveedor { Name = "Reciclando Carton", Phone = "123-456-7890", Address = "Calle Falsa 123, Ciudad A", ProductType = "Caja Re-armada" }
            };
            _context.Proveedores.AddRange(proveedores);
        }      
            await _context.SaveChangesAsync();
    }

    
}