﻿using Immunilog.Domain.Dto.Vacina;
using Immunilog.Domain.Entities;
using Immunilog.Repositories.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Immunilog.Repositories.Repositories;

public interface IVacinaRepository
{
    Task<List<VacinaDto>> GetListAsync();
    Task<VacinaDto?> GetAsync(Guid id);
    Task<Guid> CreateAsync(CreationVacinaDto data);
    Task UpdateAsync(VacinaDto data);
    Task<bool> DeleteAsync(Guid id);
}
public class VacinaRepository : IVacinaRepository
{
    public ApiBaseDbContext dbContext { get; }

    public VacinaRepository(ApiBaseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<VacinaDto>> GetListAsync()
    {
        return await dbContext.Vacina
            .AsNoTracking()
            .ProjectToType<VacinaDto>()
            .ToListAsync();
    }

    public async Task<VacinaDto?> GetAsync(Guid id)
    {
        return await dbContext.Vacina
            .AsNoTracking()
            .ProjectToType<VacinaDto>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> CreateAsync(CreationVacinaDto data)
    {
        var newVacina = new Vacina
        {
            Id = Guid.NewGuid(),
            DtCriacao = DateTime.Now,
            Nome = data.Nome,
            Descricao = data.Descricao,
            IdadeRecomendada = data.IdadeRecomendada
        };

        await dbContext.Vacina.AddAsync(newVacina);

        if (data.Doencas != null && data.Doencas.Any())
        {
            foreach (var doencaNome in data.Doencas)
            {
                var doenca = await dbContext.Doenca
                    .FirstOrDefaultAsync(d => d.Nome == doencaNome);

                if (doenca == null)
                {
                    doenca = new Doenca
                    {
                        Id = Guid.NewGuid(),
                        Nome = doencaNome
                    };

                    await dbContext.Doenca.AddAsync(doenca);
                    await dbContext.SaveChangesAsync();
                }

                var vacinaDoenca = new VacinaDoenca
                {
                    VacinaId = newVacina.Id,
                    DoencaId = doenca.Id
                };

                await dbContext.VacinaDoencas.AddAsync(vacinaDoenca);
            }
        }

        await dbContext.SaveChangesAsync();

        return newVacina.Id;
    }


    public async Task UpdateAsync(VacinaDto data)
    {
        var vacina = await dbContext.Vacina
            .FirstOrDefaultAsync(c => c.Id == data.Id);

        if (vacina == null)
        {
            throw new Exception("Vacina não encontrada.");
        }

        vacina.Nome = data.Nome;
        vacina.Descricao = data.Descricao;
        vacina.IdadeRecomendada = data.IdadeRecomendada;
        vacina.DtUpdate = DateTime.Now;

        if (data.Doencas != null && data.Doencas.Any())
        {
            var vacinasDoencas = await dbContext.VacinaDoencas
                .Where(vd => vd.VacinaId == data.Id)
                .ToListAsync();

            dbContext.VacinaDoencas.RemoveRange(vacinasDoencas);

            var doencas = await dbContext.Doenca
                .Where(d => data.Doencas.Contains(d.Nome))
                .ToListAsync();

            foreach (var doenca in doencas)
            {
                var vacinaDoenca = new VacinaDoenca
                {
                    VacinaId = vacina.Id,
                    DoencaId = doenca.Id
                };

                await dbContext.VacinaDoencas.AddAsync(vacinaDoenca);
            }
        }

        await dbContext.SaveChangesAsync();
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var vacina = await dbContext.Vacina
            .FirstOrDefaultAsync(pu => pu.Id == id);

        if (vacina == null)
            return false;

        dbContext.Vacina.Remove(vacina);
        await dbContext.SaveChangesAsync();

        return true;
    }
}