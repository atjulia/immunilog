using Immunilog.Domain.Dto.Vacina;
using Immunilog.Services.Services.Vacina;
using Immunilog.Services.Services.VacinaPessoa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Immunilog.UI.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class VacinaPessoaController : ControllerBase
{
    private readonly IVacinaPessoaService vacinaPessoaService;

    public VacinaPessoaController(IVacinaPessoaService vacinaPessoaService)
    {
        this.vacinaPessoaService = vacinaPessoaService;
    }

    [HttpPost("CreateSolicitacaoVacina")]
    public async Task<ActionResult> CreateSolicitacaoVacina([FromBody] CreationVacinaPessoaDto vacina)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await vacinaPessoaService.CreateSolicitacaoVacina(vacina);

        return Ok(id);
    }

    [HttpPost("GetVacinasByPessoaId")]
    public async Task<IActionResult> GetVacinasByPessoaId([FromBody] Guid pessoaId)
    {
        if (pessoaId == Guid.Empty)
        {
            return BadRequest("O ID da pessoa é inválido.");
        }

        var vacinas = await vacinaPessoaService.GetVacinasByPessoaId(pessoaId);

        if (vacinas == null || !vacinas.Any())
        {
            return NotFound("Nenhuma vacina encontrada para o ID da pessoa fornecido.");
        }

        return Ok(vacinas);
    }

    [HttpPut("UpdateVacinaPessoa")]
    public async Task<ActionResult> UpdateVacinaPessoa([FromBody] VacinaPessoaDto vacina)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await vacinaPessoaService.UpdateVacinaPessoa(vacina);

        return Ok(id);
    }
}
