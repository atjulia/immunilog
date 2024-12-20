﻿using Immunilog.Domain.Dto.Vacina;
using Immunilog.Domain.Entities;
using Immunilog.Repositories.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using Z.EntityFramework.Plus;

namespace Immunilog.Repositories.Repositories;

public interface IVacinaPessoaRepository
{
    Task<Guid> CreateSolicitacaoVacina(CreationVacinaPessoaDto model);
    Task UpdateVacinaPessoa(VacinaPessoaDto model);
    Task<List<VacinaPessoa>> GetVacinasByPessoaId(Guid pessoaId);
}
public class VacinaPessoaRepository : IVacinaPessoaRepository
{
    public ApiBaseDbContext dbContext { get; }

    public VacinaPessoaRepository(ApiBaseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Guid> CreateSolicitacaoVacina(CreationVacinaPessoaDto model)
    {
        //var vacina = await dbContext.Vacina.FirstOrDefaultAsync(c => c.Id == model.Id);

        var newVacinaPessoa = new VacinaPessoa
        {
            Id = Guid.NewGuid(),
            PessoaId = model.PessoaId,
            VacinaId = model.VacinaId,
            DtCriacao = DateTime.Now,
            DtAplicacao = DateTime.ParseExact(model.DtAplicacao, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
            Reacao = model.Reacao,
            ReacaoOutros = model.ReacaoOutros,
            Fabricante = model.Fabricante,
            LocalAplicacao = model.LocalAplicacao,
            LoteVacina = model.LoteVacina
        };

        await dbContext.VacinaPessoa.AddAsync(newVacinaPessoa);
        await dbContext.SaveChangesAsync();

        return newVacinaPessoa.Id;
    }
    public async Task<List<VacinaPessoa>> GetVacinasByPessoaId(Guid pessoaId)
    {
        var vacinaPessoas = await dbContext.VacinaPessoa
            .ProjectToType<VacinaPessoa>()
            .AsNoTracking()
            .Where(vp => vp.PessoaId == pessoaId)
            .ToListAsync();

        return vacinaPessoas;
    }
    public async Task UpdateVacinaPessoa(VacinaPessoaDto model)
    => await dbContext.VacinaPessoa
        .AsNoTracking()
        .Where(c => c.Id == model.Id)
        .UpdateAsync(c => new VacinaPessoa
        {
            DtUpdate = DateTime.Now,
            Reacao = c.Reacao,
            ReacaoOutros = c.ReacaoOutros
        });

}
