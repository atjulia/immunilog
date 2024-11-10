using Immunilog.Domain.Dto.Usuario;
using Immunilog.Domain.Dto.Vacina;
using Immunilog.Domain.Entities;
using Immunilog.Services.Services.Autenticacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Immunilog.UI.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Message = "Dados de entrada inválidos" });
        }

        var token = await _authService.Authenticate(request.Email, request.Senha);
        if (token == null)
        {
            return Unauthorized(new { Message = "Credenciais inválidas" });
        }

        return Ok(new { Token = token });
    }

}

public class LoginRequest
{
    public string Email { get; set; }
    public string Senha { get; set; }
}

