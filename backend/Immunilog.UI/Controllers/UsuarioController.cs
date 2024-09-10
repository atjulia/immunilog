using Immunilog.Domain.Dto.Usuario;
using Immunilog.Services.Services.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Immunilog.UI.Controllers;

[ApiController]
[AllowAnonymous]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("usuarios/")]
    public async Task<ActionResult<UsuarioDto?>> GetUsuarios()
    {
        var usuario = await _usuarioService.GetListAsync();

        return Ok(usuario);
    }

    [HttpPost("usuarios/")]
    public async Task<ActionResult> CreateUsuario([FromBody] CreationUsuarioDto usuario)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await _usuarioService.CreateAsync(usuario);

        return Ok(id);
    }

    [HttpPut("usuarios/")]
    public async Task<ActionResult> UpdateUsuario([FromBody] UsuarioDto usuarioDto)
    {
        if (!ModelState.IsValid) return BadRequest();
            
        var id = await _usuarioService.UpdateAsync(usuarioDto);

        return Ok(id);
    }

    [HttpGet("usuarios/{usuarioId}")]
    public async Task<ActionResult> GetUsuario(Guid usuarioId)
    {
        var usuario = await _usuarioService.GetAsync(usuarioId);

        return Ok(usuario);
    }

    [HttpDelete("usuarios/{usuarioId}")]
    public async Task<ActionResult> DeleteUsuario(Guid usuarioId)
    {
        await _usuarioService.DeleteAsync(usuarioId);

        return Ok();
    }
}
