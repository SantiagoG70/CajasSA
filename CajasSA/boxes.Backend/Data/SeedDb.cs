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
        if (!_context.Roles.Any())
        {
            _context.Roles.Add(new Rol { Name = "Administrador" });
            _context.Roles.Add(new Rol { Name = "Cliente" });
            _context.Roles.Add(new Rol { Name = "Empleado" });
        }

        await _context.SaveChangesAsync();
    }
}