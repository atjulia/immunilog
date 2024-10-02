using Immunilog.Domain.Dto.Vacina;
using Immunilog.Services.Services.Vacina;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Immunilog.UI.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class VacinaController : ControllerBase
{
    private readonly IVacinaService vacinaService;

    public VacinaController(IVacinaService vacinaService)
    {
        this.vacinaService = vacinaService;
    }

    [HttpGet("GetVacinas")]
    public async Task<ActionResult<VacinaDto?>> GetVacinas()
    {
        var vacina = await vacinaService.GetListAsync();

        return Ok(vacina);
    }

    [HttpPost("CreateVacina")]
    public async Task<ActionResult> CreateVacina([FromBody] CreationVacinaDto vacina)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await vacinaService.CreateAsync(vacina);

        return Ok(id);
    }

    [HttpPut("UpdateVacina")]
    public async Task<ActionResult> UpdateVacina([FromBody] VacinaDto vacina)
    {
        if (!ModelState.IsValid) return BadRequest();
            
        var id = await vacinaService.UpdateAsync(vacina);

        return Ok(id);
    }

    [HttpGet("GetVacinaById/{vacinaId}")]
    public async Task<ActionResult> GetVacinaById(Guid vacinaId)
    {
        var usuario = await vacinaService.GetAsync(vacinaId);

        return Ok(usuario);
    }
    [HttpGet("GetVacinaByIdadePessoa/{pessoaId}")]
    public async Task<ActionResult> GetVacinaByIdadePessoa(Guid pessoaId, [FromQuery] string param)
    {
        var vacinas = await vacinaService.GetVacinaByIdadePessoa(pessoaId, param);

        return Ok(vacinas);
    }

    [HttpDelete("DeleteVacina/{vacinaId}")]
    public async Task<ActionResult> DeleteVacina(Guid vacinaId)
    {
        await vacinaService.DeleteAsync(vacinaId);

        return Ok();
    }
}
