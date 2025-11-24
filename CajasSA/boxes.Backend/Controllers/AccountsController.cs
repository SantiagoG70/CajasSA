using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("/api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IUsuariosUnitOfWork _usuariosUnitOfWork;
    private readonly IConfiguration _configuration;

    public AccountsController(IUsuariosUnitOfWork usuariosUnitOfWork, IConfiguration configuration)
    {
        _usuariosUnitOfWork = usuariosUnitOfWork;
        _configuration = configuration;
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] UsuariosDTO model)
    {
        Usuario usuario = model;
        var result = await _usuariosUnitOfWork.AddUsuarioAsync(usuario, model.Password);
        if (result.Succeeded)
        {
            await _usuariosUnitOfWork.AddUsuarioToRoleAsync(usuario, usuario.UserType.ToString());
            return Ok(BuildToken(usuario));
        }

        return BadRequest(result.Errors.FirstOrDefault());
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
    {
        var result = await _usuariosUnitOfWork.LoginAsync(model);
        if (result.Succeeded)
        {
            var user = await _usuariosUnitOfWork.GetUsuarioAsync(model.Email);
            return Ok(BuildToken(user));
        }

        return BadRequest("Email o contraseña incorrectos.");
    }

    private TokenDTO BuildToken(Usuario usuario)
    {
        var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.Email!),
                new(ClaimTypes.Role, usuario.UserType.ToString()),
                new("Document", usuario.Document),
                new("FirstName", usuario.FirstName),
                new("LastName", usuario.LastName),
                new("Address", usuario.Address),
                new("Photo", usuario.Photo ?? string.Empty)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddDays(30);
        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new TokenDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}