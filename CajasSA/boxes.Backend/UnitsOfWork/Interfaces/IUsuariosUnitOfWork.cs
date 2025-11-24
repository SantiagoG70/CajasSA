using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Identity;

namespace boxes.Backend.UnitsOfWork.Interfaces;

public interface IUsuariosUnitOfWork
{
    Task<SignInResult> LoginAsync(LoginDTO model);

    Task LogoutAsync();

    Task<Usuario> GetUsuarioAsync(string email);

    Task<IdentityResult> AddUsuarioAsync(Usuario usuario, string password);

    Task CheckRoleAsync(string roleName);

    Task AddUsuarioToRoleAsync(Usuario usuario, string roleName);

    Task<bool> IsUsuarioInRoleAsync(Usuario usuario, string roleName);
}