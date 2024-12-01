using Immunilog.Domain.Dto.Vacina;
using Immunilog.Repositories.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Immunilog.Services.Services.VacinaPessoa;

public interface IVacinaPessoaService
{
    Task<Guid> CreateSolicitacaoVacina(CreationVacinaPessoaDto model);
    Task<bool> UpdateVacinaPessoa(VacinaPessoaDto model);
    Task<List<Immunilog.Domain.Entities.VacinaPessoa>> GetVacinasByPessoaId(Guid pessoaId);

}

public class VacinaPessoaService : IVacinaPessoaService
{
    private readonly IVacinaPessoaRepository vacinaPessoaRepository;


    public VacinaPessoaService(IVacinaPessoaRepository vacinaPessoaRepository)
    {
        this.vacinaPessoaRepository = vacinaPessoaRepository;
    }
    public async Task<Guid> CreateSolicitacaoVacina(CreationVacinaPessoaDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        var vacinaPessoaId = await vacinaPessoaRepository.CreateSolicitacaoVacina(model);

        return vacinaPessoaId;
    }
    public async Task<List<Immunilog.Domain.Entities.VacinaPessoa>> GetVacinasByPessoaId(Guid pessoaId)
    {
        if (pessoaId == Guid.Empty) throw new ValidationException("Dados inválidos");

        var vacinaPessoaId = await vacinaPessoaRepository.GetVacinasByPessoaId(pessoaId);

        return vacinaPessoaId;
    }
    public async Task<bool> UpdateVacinaPessoa(VacinaPessoaDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        await vacinaPessoaRepository.UpdateVacinaPessoa(model);

        return true;
    }
}
