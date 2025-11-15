using boxes.Backend.Repositories.Interfaces;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Identity;

namespace boxes.Backend.UnitsOfWork.Implementations
{
    public class UsuariosUnitOfWork : IUsuariosUnitOfWork
    {
        private readonly IUsuariosRepository _usuariosRepository;
        public UsuariosUnitOfWork(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }
        public async Task<IdentityResult> AddUsuarioAsync(Usuario user, string password) => 
            await _usuariosRepository.AddUsuarioAsync(user, password);


        public Task AddUsuarioToRoleAsync(Usuario user, string roleName) =>
            _usuariosRepository.AddUsuarioToRoleAsync(user, roleName);


        public Task CheckRoleAsync(string roleName) =>
            _usuariosRepository.CheckRoleAsync(roleName);


        public Task<Usuario> GetUsuarioAsync(string email) =>
            _usuariosRepository.GetUsuarioAsync(email)!;


        public Task<bool> IsUsuarioInRoleAsync(Usuario user, string roleName) =>
            _usuariosRepository.IsUsuarioInRoleAsync(user, roleName);
        public async Task<SignInResult> LoginAsync(LoginDTO model) => await _usuariosRepository.LoginAsync(model);

        public async Task LogoutAsync() => await _usuariosRepository.LogoutAsync();

    }
}
