using Immunilog.Domain.Dto.Vacina;
using Immunilog.Repositories.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Immunilog.Services.Services.Vacina;

public interface IVacinaService
{
    Task<List<VacinaDto>> GetListAsync();
    Task<VacinaDto?> GetAsync(Guid id);
    Task<List<Immunilog.Domain.Entities.Vacina>> GetVacinaByIdadePessoa(Guid id);
    Task<Guid> CreateAsync(CreationVacinaDto model);
    Task<bool> UpdateAsync(VacinaDto model);
    Task<bool> DeleteAsync(Guid id);
}

public class VacinaService : IVacinaService
{
    private readonly IVacinaRepository vacinaRepository;


    public VacinaService(IVacinaRepository vacinaRepository)
    {
        this.vacinaRepository = vacinaRepository;
    }

    public async Task<List<VacinaDto>> GetListAsync()
        => await vacinaRepository.GetListAsync();

    public async Task<VacinaDto?> GetAsync(Guid id)
        => await vacinaRepository.GetAsync(id);
    public async Task<List<Immunilog.Domain.Entities.Vacina>> GetVacinaByIdadePessoa(Guid id)
    {
        var listVacina = await vacinaRepository.GetVacinaByIdadePessoa(id);
        return listVacina;
    }


    public async Task<bool> DeleteAsync(Guid id)
    => await vacinaRepository.DeleteAsync(id);

    public async Task<Guid> CreateAsync(CreationVacinaDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        var vacinaId = await vacinaRepository.CreateAsync(model);

        return vacinaId;
    }

    public async Task<bool> UpdateAsync(VacinaDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        await vacinaRepository.UpdateAsync(model);

        return true;
    }
}
