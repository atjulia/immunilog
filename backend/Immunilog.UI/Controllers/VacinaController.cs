using Immunilog.Domain.Dto.Vacina;
using Immunilog.Services.Services.Vacina;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Immunilog.UI.Controllers;

[ApiController]
[AllowAnonymous]
public class VacinaController : ControllerBase
{
    private readonly IVacinaService vacinaService;

    public VacinaController(IVacinaService vacinaService)
    {
        this.vacinaService = vacinaService;
    }

    [HttpGet("vacina/")]
    public async Task<ActionResult<VacinaDto?>> GetVacinas()
    {
        var vacina = await vacinaService.GetListAsync();

        return Ok(vacina);
    }

    [HttpPost("vacina/")]
    public async Task<ActionResult> CreateVacina([FromBody] CreationVacinaDto vacina)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await vacinaService.CreateAsync(vacina);

        return Ok(id);
    }

    [HttpPut("vacina/")]
    public async Task<ActionResult> UpdateVacina([FromBody] VacinaDto vacina)
    {
        if (!ModelState.IsValid) return BadRequest();
            
        var id = await vacinaService.UpdateAsync(vacina);

        return Ok(id);
    }

    [HttpGet("vacina/{vacinaId}")]
    public async Task<ActionResult> GetVacina(Guid vacinaId)
    {
        var usuario = await vacinaService.GetAsync(vacinaId);

        return Ok(usuario);
    }

    [HttpDelete("vacina/{vacinaId}")]
    public async Task<ActionResult> DeleteVacina(Guid vacinaId)
    {
        await vacinaService.DeleteAsync(vacinaId);

        return Ok();
    }
}
