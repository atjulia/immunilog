using Immunilog.Domain.Dto.Pessoa;
using Immunilog.Services.Services.Pessoa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService pessoaService;

    public PessoaController(IPessoaService pessoaService)
    {
        this.pessoaService = pessoaService;
    }

    [HttpPost("CreatePessoa")]
    public async Task<ActionResult> CreatePessoa([FromBody] CreationPessoaDto pessoaDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await pessoaService.CreateAsync(pessoaDto);

        return Ok(id);
    }

    [HttpPut("UpdatePessoa")]
    public async Task<ActionResult> UpdatePessoa([FromBody] PessoaDto pessoaDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await pessoaService.UpdateAsync(pessoaDto);

        return Ok(id);
    }
    [HttpGet("getPessoaById/{pessoaId}")]
    public async Task<ActionResult> getPessoaById(Guid pessoaId)
    {
        var pessoas = await pessoaService.GetPessoaByIdAsync(pessoaId);

        return Ok(pessoas);
    }

    [HttpGet("GetPessoasByUsuarioId/{usuarioId}")]
    public async Task<ActionResult> GetPessoasByUsuarioId(Guid usuarioId)
    {
        var pessoas = await pessoaService.GetPessoasByUsuarioId(usuarioId);

        return Ok(pessoas);
    }

    [HttpDelete("DeletePessoa/{pessoaId}")]
    public async Task<ActionResult> DeletePessoa(Guid pessoaId)
    {
        await pessoaService.DeleteAsync(pessoaId);

        return Ok();
    }
}
