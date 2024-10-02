using Immunilog.Domain.Dto.Pessoa;
using Immunilog.Repositories.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Immunilog.Services.Services.Pessoa;

public interface IPessoaService
{
    Task<List<PessoaDto>> GetPessoasByUsuarioId(Guid id);
    Task<Guid> CreateAsync(CreationPessoaDto model);
    Task<bool> UpdateAsync(PessoaDto model);
    Task<PessoaDto> GetPessoaByIdAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);

}

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository pessoaRepository;


    public PessoaService(IPessoaRepository pessoaRepository)
    {
        this.pessoaRepository = pessoaRepository;
    }

    public async Task<PessoaDto> GetPessoaByIdAsync(Guid id)
    {
        if (id == Guid.Empty) throw new ValidationException("ID inválido");

        var pessoa = await pessoaRepository.GetPessoaByIdAsync(id);

        if (pessoa == null) throw new ValidationException("Pessoa não encontrada");

        return pessoa;
    }
    public async Task<List<PessoaDto>> GetPessoasByUsuarioId(Guid id)
    => await pessoaRepository.GetPessoasByUsuarioId(id);

    public async Task<bool> DeleteAsync(Guid id)
    => await pessoaRepository.DeleteAsync(id);

    public async Task<Guid> CreateAsync(CreationPessoaDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        var pessoaId = await pessoaRepository.CreateAsync(model);

        return pessoaId;
    }

    public async Task<bool> UpdateAsync(PessoaDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        await pessoaRepository.UpdateAsync(model);

        return true;
    }
}
