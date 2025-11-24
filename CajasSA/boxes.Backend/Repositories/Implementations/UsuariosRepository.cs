using boxes.Backend.Repositories.Interfaces;
using Boxes.Backend.Data;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace boxes.Backend.Repositories.Implementations;

public class UsuariosRepository : IUsuariosRepository
{
    private readonly DataContext _context;
    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<Usuario> _signInManager;

    public UsuariosRepository(DataContext context, UserManager<Usuario> userManager,
        RoleManager<IdentityRole> roleManager, SignInManager<Usuario> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<SignInResult> LoginAsync(LoginDTO model)
    {
        return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> AddUsuarioAsync(Usuario user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task AddUsuarioToRoleAsync(Usuario user, string roleName)
    {
        await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task CheckRoleAsync(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleName
            });
        }
    }

    public async Task<Usuario> GetUsuarioAsync(string email)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Email == email);
        return usuario!;
    }

    public async Task<bool> IsUsuarioInRoleAsync(Usuario usuario, string roleName)
    {
        return await _userManager.IsInRoleAsync(usuario, roleName);
    }
}