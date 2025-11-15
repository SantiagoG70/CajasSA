using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Boxes.Shared.Enums;
using System.Net;

namespace Boxes.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IUsuariosUnitOfWork _usersUnitOfWork;


    public SeedDb(DataContext context , IUsuariosUnitOfWork usuariosUnitOfWork)
    {
        _context = context;
        _usersUnitOfWork = usuariosUnitOfWork;  
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckProveedoresAsync();
        await CheckRolesAsync();
        await CheckUserAsync("1010", "Tomas", "Arias", "tomasariasuribe302@gmail.com", "316 582 6289", "La isla", UserType.Admin);

    }
    private async Task CheckRolesAsync()
    {
        await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
        await _usersUnitOfWork.CheckRoleAsync(UserType.Cliente.ToString());
        await _usersUnitOfWork.CheckRoleAsync(UserType.Empleado.ToString());
    }

    private async Task<Usuario> CheckUserAsync(string document, string name, string lastName, string email, string phone, string address, UserType userType)
    {
        var user = await _usersUnitOfWork.GetUsuarioAsync(email);
        if (user == null)
        {
            user = new Usuario
            {
                Name = name,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                UserType = userType,
            };

            await _usersUnitOfWork.AddUsuarioAsync(user, "123456");
            await _usersUnitOfWork.AddUsuarioToRoleAsync(user, userType.ToString());
        }

        return user;
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