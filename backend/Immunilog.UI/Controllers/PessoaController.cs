using Immunilog.Domain.Dto.Pessoa;
using Immunilog.Services.Services.Pessoa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[AllowAnonymous]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService pessoaService;

    public PessoaController(IPessoaService pessoaService)
    {
        this.pessoaService = pessoaService;
    }

    [HttpGet("pessoa/")]
    public async Task<ActionResult<PessoaDto?>> GetPessoas()
    {
        var pessoa = await pessoaService.GetListAsync();

        return Ok(pessoa);
    }

    [HttpPost("pessoa/")]
    public async Task<ActionResult> CreatePessoa([FromBody] CreationPessoaDto pessoaDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await pessoaService.CreateAsync(pessoaDto);

        return Ok(id);
    }

    [HttpPut("pessoa/")]
    public async Task<ActionResult> UpdatePessoa([FromBody] PessoaDto pessoaDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await pessoaService.UpdateAsync(pessoaDto);

        return Ok(id);
    }

    [HttpGet("pessoa/{pessoaId}")]
    public async Task<ActionResult> GetPessoa(Guid pessoaId)
    {
        var product = await pessoaService.GetAsync(pessoaId);

        return Ok(product);
    }

    [HttpDelete("pessoa/{pessoaId}")]
    public async Task<ActionResult> DeletePessoa(Guid pessoaId)
    {
        await pessoaService.DeleteAsync(pessoaId);

        return Ok();
    }
}
