using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Identity;

namespace boxes.Backend.Repositories.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<Usuario?> GetUsuarioAsync(string email); //obtener usuario por email
        Task<IdentityResult> AddUsuarioAsync(Usuario user, string password); //agregar usuario

        Task CheckRoleAsync(string roleName); //verificar rol

        Task AddUsuarioToRoleAsync(Usuario user, string roleName); //agregar usuario a rol

        Task<bool> IsUsuarioInRoleAsync(Usuario user, string roleName); //verificar si el usuario esta en un rol

        Task<SignInResult> LoginAsync(LoginDTO model); //login usuario

        Task LogoutAsync(); //logout usuario


    }
}
