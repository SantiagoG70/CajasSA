using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Identity;

namespace boxes.Backend.UnitsOfWork.Interfaces
{
    public interface IUsuariosUnitOfWork
    {
        Task<Usuario> GetUsuarioAsync(string email); 

        Task<IdentityResult> AddUsuarioAsync(Usuario user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUsuarioToRoleAsync(Usuario user, string roleName);

        Task<bool> IsUsuarioInRoleAsync(Usuario user, string roleName);

        Task<SignInResult> LoginAsync(LoginDTO model);

        Task LogoutAsync();

    }
} 
