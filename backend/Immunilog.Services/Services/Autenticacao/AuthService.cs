using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Immunilog.Domain.Dto.Usuario;
using Immunilog.Domain.Dto.Vacina;
using Immunilog.Domain.Entities;
using Immunilog.Repositories.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Z.Expressions;

namespace Immunilog.Services.Services.Autenticacao;

public interface IAuthService
{
    Task<UsuarioCredentials?> Authenticate(string email, string senha);
}

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioCredentials> Authenticate(string email, string senha)
    {
        var user = await _usuarioRepository.GetUserByEmail(email);

        if (user == null || user.Senha != senha)
            return null;

        var userCredentials = new UsuarioCredentials
        {
            Nome = user.Nome,
            Email = user.Email,
            UsuarioId = user.Id
        };

        return userCredentials;
    }
}
