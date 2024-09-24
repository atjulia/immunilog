﻿using Immunilog.Domain.Dto.Vacina;
using Immunilog.Repositories.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Immunilog.Services.Services.VacinaPessoa;

public interface IVacinaPessoaService
{
    Task<Guid> CreateSolicitacaoVacina(CreationVacinaPessoaDto model);
    Task<Guid> CreateVacinaPessoa(CreationVacinaPessoaDto model);
    Task<bool> UpdateVacinaPessoa(VacinaPessoaDto model);

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

        //VALIDACOES

        var vacinaPessoaId = await vacinaPessoaRepository.CreateSolicitacaoVacina(model);

        return vacinaPessoaId;
    }
    public async Task<Guid> CreateVacinaPessoa(CreationVacinaPessoaDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        var vacinaPessoaId = await vacinaPessoaRepository.CreateVacinaPessoa(model);

        return vacinaPessoaId;
    }
    public async Task<bool> UpdateVacinaPessoa(VacinaPessoaDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        await vacinaPessoaRepository.UpdateVacinaPessoa(model);

        return true;
    }
}