using Boxes.Shared.Entites;
using System.Net;

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
            _context.Roles.Add(new Rol { Name = "Administrador" });
            _context.Roles.Add(new Rol { Name = "Empleado" });
            _context.Roles.Add(new Rol { Name = "Cliente" });
        }

        await _context.SaveChangesAsync();
    }

    public async Task CheckProveedoresAsync()
    {
        if (!_context.Proveedores.Any()) // Si no hay proveedores, los creamos
        {
            _context.Proveedores.Add(new Proveedor { Name = "Reciclando Carton", Phone = "123-456-7890", Address = "Calle Falsa 123, Ciudad A", ProductType = "Caja Re-armada" });
        }
        await _context.SaveChangesAsync();
    }
}