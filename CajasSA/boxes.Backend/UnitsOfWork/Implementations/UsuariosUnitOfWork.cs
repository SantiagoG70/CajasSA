using boxes.Backend.Repositories.Interfaces;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Identity;

namespace Boxes.Backend.UnitsOfWork.Implementations;

public class UsuariosUnitOfWork : IUsuariosUnitOfWork
{
    private readonly IUsuariosRepository _UsuariosRepository;

    public UsuariosUnitOfWork(IUsuariosRepository UsuariosRepository)
    {
        _UsuariosRepository = UsuariosRepository;
    }

    public async Task<SignInResult> LoginAsync(LoginDTO model) => await _UsuariosRepository.LoginAsync(model);

    public async Task LogoutAsync() => await _UsuariosRepository.LogoutAsync();

    public async Task<IdentityResult> AddUsuarioAsync(Usuario Usuario, string password) => await _UsuariosRepository.AddUsuarioAsync(Usuario, password);

    public async Task AddUsuarioToRoleAsync(Usuario Usuario, string roleName) => await _UsuariosRepository.AddUsuarioToRoleAsync(Usuario, roleName);

    public async Task CheckRoleAsync(string roleName) => await _UsuariosRepository.CheckRoleAsync(roleName);

    public async Task<Usuario> GetUsuarioAsync(string email) => await _UsuariosRepository.GetUsuarioAsync(email);

    public async Task<bool> IsUsuarioInRoleAsync(Usuario Usuario, string roleName) => await _UsuariosRepository.IsUsuarioInRoleAsync(Usuario, roleName);
}