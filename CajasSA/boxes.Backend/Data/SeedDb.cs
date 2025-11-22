using boxes.Backend.Helpers;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Boxes.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.InteropServices;

namespace Boxes.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IUsuariosUnitOfWork _usersUnitOfWork;
    private readonly IFileStorage _fileStorage;


    public SeedDb(DataContext context , IUsuariosUnitOfWork usuariosUnitOfWork, IFileStorage fileStorage)
    {
        _context = context;
        _usersUnitOfWork = usuariosUnitOfWork;  
        _fileStorage = fileStorage;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckProveedoresAsync();
        await CheckRolesAsync();
        await CheckCategoriesAsync();
        await CheckProductosAsync();
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

    private async Task CheckCategoriesAsync()
    {
        if (!_context.Categorias.Any())
        {
            _context.Categorias.Add(new Categoria { Name = "Re-armadas" });
            _context.Categorias.Add(new Categoria { Name = "Recicladas" });
            _context.Categorias.Add(new Categoria { Name = "Compuestas" });
            await _context.SaveChangesAsync();
        }
    }


    private async Task CheckProductosAsync() {
        if (!_context.Productos.Any()) {
            await AddProductosAsync("Caja re-armada", 50 , 1000 ,"carton re armado" , 100 , 1 ,new List<string>() {"Re-armadas" } ,new List<string>() { "CAJA-REGULAR-PEQUENA.png"});
            await AddProductosAsync("Caja reciclada", 50 , 1000 ,"carton reciclado" , 200 , 1 ,new List<string>() {"Recicladas" } ,new List<string>() { "CAJA-REGULAR-PEQUENA.png"});
            await _context.SaveChangesAsync();
        }
    }

    private async Task AddProductosAsync(string name, int quantity, decimal price, string type, int max , int min ,List<string> categories,List<string> images)
    {
        Producto producto = new()
        {
            Description = "Caja basica que se armo empleando reciclaje",
            Name = name,
            Quantity = quantity,
            Price = price,
            Type = type,
            Max = max,
            Min = min,
            ProductCategories = new List<ProductCategory>(),
            ProductImages = new List<ProductImage>(),
            ProveedorId = _context.Proveedores.First().Id
        };

        foreach (var categoryName in categories)
        {
            var category = await _context.Categorias.FirstOrDefaultAsync(c => c.Name == categoryName);
            if (category != null)
            {
                producto.ProductCategories.Add(new ProductCategory { Category = category });
            }
        }


        foreach (string? image in images)
        {
            string filePath;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                filePath = $"{Environment.CurrentDirectory}\\Images\\Productos\\{image}";
            }
            else
            {
                filePath = $"{Environment.CurrentDirectory}/Images/Productos/{image}";
            }
            var fileBytes = File.ReadAllBytes(filePath);
            var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "Productos");
            producto.ProductImages.Add(new ProductImage { Image = imagePath });
        }

        _context.Productos.Add(producto);
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