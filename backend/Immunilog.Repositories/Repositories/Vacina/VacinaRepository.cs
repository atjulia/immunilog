using Immunilog.Domain.Dto.Vacina;
using Immunilog.Domain.Entities;
using Immunilog.Domain.Enums;
using Immunilog.Repositories.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;
using Z.EntityFramework.Plus;

namespace Immunilog.Repositories.Repositories;

public interface IVacinaRepository
{
    Task<List<VacinaDto>> GetListAsync();
    Task<VacinaDto?> GetAsync(Guid id);
    Task<List<Immunilog.Domain.Entities.Vacina>> GetVacinaByIdadePessoa(Guid pessoaId, string param);
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
    public async Task<List<Vacina>> GetVacinaByIdadePessoa(Guid pessoaId, string param)
    {
        // Busca a pessoa pelo pessoaId
        var pessoa = await dbContext.Pessoa
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == pessoaId);

        if (pessoa == null)
        {
            throw new Exception("Pessoa não encontrada");
        }

        // Cálculo da idade da pessoa em anos e meses
        var dataAtual = DateTime.Now;
        int anos = dataAtual.Year - pessoa.DtNascimento.Year;
        if (dataAtual < pessoa.DtNascimento.AddYears(anos))
        {
            anos--;
        }

        int meses = (dataAtual.Year - pessoa.DtNascimento.Year) * 12 + dataAtual.Month - pessoa.DtNascimento.Month;

        // Inicializa a consulta de vacinas com base na idade da pessoa
        var vacinasQuery = dbContext.Vacina
            .AsNoTracking()
            .Where(v =>
                (v.IdadeRecomendada >= anos ||
                (v.IdadeRecomendada < 1 && (v.IdadeRecomendada * 100) <= meses)))
            .OrderBy(v => v.Nome)
            .AsQueryable();

        // Se o param for "filtroVacina", filtra as vacinas que ainda não estão na tabela VacinaPessoa para essa pessoa
        if (param == "filtroVacina")
        {
            var vacinasJaRegistradas = await dbContext.VacinaPessoa
                .AsNoTracking()
                .Where(vp => vp.PessoaId == pessoaId)
                .Select(vp => vp.VacinaId)
                .ToListAsync();

            // Exclui as vacinas que já foram registradas na VacinaPessoa
            vacinasQuery = vacinasQuery.Where(v => !vacinasJaRegistradas.Contains(v.Id));
        }

        // Projeta para o tipo Vacina e retorna a lista final
        var vacinas = await vacinasQuery
            .ProjectToType<Vacina>()
            .ToListAsync();

        return vacinas;
    }


    public async Task<Guid> CreateAsync(CreationVacinaDto data)
    {
        DoseVacina tipoDoseEnum = (DoseVacina)Enum.ToObject(typeof(DoseVacina), data.TipoDose);
        TipoCalendario tipoCalendarioEnum = (TipoCalendario)Enum.ToObject(typeof(TipoCalendario), data.TipoCalendario);

        var newVacina = new Vacina
        {
            Id = Guid.NewGuid(),
            DtCriacao = DateTime.Now,
            Nome = data.Nome,
            Descricao = data.Descricao,
            IdadeRecomendada = data.IdadeRecomendada,
            TipoCalendario = GetEnumDescription(tipoCalendarioEnum),
            TipoDose = GetEnumDescription(tipoDoseEnum),
            TipoDoseObs = data.TipoDoseObs
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

        DoseVacina tipoDoseEnum = (DoseVacina)Enum.ToObject(typeof(DoseVacina), data.TipoDose);
        TipoCalendario tipoCalendarioEnum = (TipoCalendario)Enum.ToObject(typeof(TipoCalendario), data.TipoCalendario);

        vacina.Nome = data.Nome;
        vacina.Descricao = data.Descricao;
        vacina.IdadeRecomendada = data.IdadeRecomendada;
        vacina.TipoCalendario = GetEnumDescription(tipoCalendarioEnum);
        vacina.TipoDose = GetEnumDescription(tipoDoseEnum);
        vacina.TipoDoseObs = data.TipoDoseObs;
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
    //TODO: colocar em um helper
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute == null ? value.ToString() : attribute.Description;
    }
}
