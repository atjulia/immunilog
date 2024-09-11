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

    [HttpGet("GetPessoas")]
    public async Task<ActionResult<PessoaDto?>> GetPessoas()
    {
        var pessoa = await pessoaService.GetListAsync();

        return Ok(pessoa);
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

    [HttpGet("GetPessoaById/{pessoaId}")]
    public async Task<ActionResult> GetPessoaById(Guid pessoaId)
    {
        var product = await pessoaService.GetAsync(pessoaId);

        return Ok(product);
    }

    [HttpDelete("DeletePessoa/{pessoaId}")]
    public async Task<ActionResult> DeletePessoa(Guid pessoaId)
    {
        await pessoaService.DeleteAsync(pessoaId);

        return Ok();
    }
}
