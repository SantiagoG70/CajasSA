using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;

namespace boxes.Backend.Repositories.Interfaces;

public interface IUsuariosRepository
{
    Task<SignInResult> LoginAsync(LoginDTO model);

    Task LogoutAsync();

    Task<Usuario> GetUsuarioAsync(string email);

    Task<IdentityResult> AddUsuarioAsync(Usuario user, string password);

    Task CheckRoleAsync(string roleName);

    Task AddUsuarioToRoleAsync(Usuario user, string roleName);

    Task<bool> IsUsuarioInRoleAsync(Usuario user, string roleName);
}