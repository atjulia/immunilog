using Immunilog.Domain.Dto.Usuario;
using Immunilog.Services.Services.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Immunilog.UI.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("GetUsuarios")]
    public async Task<ActionResult<UsuarioDto?>> GetUsuarios()
    {
        var usuario = await _usuarioService.GetListAsync();

        return Ok(usuario);
    }

    [HttpPost("CreateUsuario")]
    public async Task<ActionResult> CreateUsuario([FromBody] CreationUsuarioDto usuario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _usuarioService.CreateAsync(usuario);

        return Ok(id);
    }

    [HttpPut("UpdateUsuario")]
    public async Task<IActionResult> UpdateUsuario([FromBody] UsuarioDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _usuarioService.UpdateAsync(dto);

        if (result)
        {
            return Ok(dto.Id);
        }

        return BadRequest("Não foi possível atualizar o usuário.");
    }

    [HttpGet("GetUsuarioById/{usuarioId}")]
    public async Task<ActionResult> GetUsuarioById(Guid usuarioId)
    {
        var usuario = await _usuarioService.GetUsuarioById(usuarioId);

        return Ok(usuario);
    }

    [HttpDelete("DeleteUsuario/{usuarioId}")]
    public async Task<ActionResult> DeleteUsuario(Guid usuarioId)
    {
        await _usuarioService.DeleteAsync(usuarioId);

        return Ok();
    }
}
