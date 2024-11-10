using Immunilog.Domain.Dto.Pessoa;
using Immunilog.Domain.Dto.Vacina;
using Immunilog.Domain.Entities;
using Immunilog.Domain.Enums;
using Immunilog.Repositories.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;
using Z.EntityFramework.Plus;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Immunilog.Repositories.Repositories;

public interface IPessoaRepository
{
    Task<List<PessoaDto>> GetPessoasByUsuarioId(Guid id);
    Task<Guid> CreateAsync(CreationPessoaDto data);
    Task<PessoaDto> GetPessoaByIdAsync(Guid id);
    Task UpdateAsync(PessoaDto data);
    Task<bool> DeleteAsync(Guid id);
}
public class PessoaRepository : IPessoaRepository
{
    public ApiBaseDbContext dbContext { get; }

    public PessoaRepository(ApiBaseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<PessoaDto> GetPessoaByIdAsync(Guid id)
    {
        var pessoa = await dbContext.Pessoa
                                   .AsNoTracking()
                                   .Include(p => p.Vacinas)
                                   .FirstOrDefaultAsync(p => p.Id == id);
        if (pessoa == null)
            return null;

        var pessoaDto = new PessoaDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Cpf = pessoa.Cpf,
            TipoPessoa = pessoa.TipoPessoa,
            DtNascimento = pessoa.DtNascimento,
            DtCriacao = pessoa.DtCriacao,
            DtUpdate = pessoa.DtUpdate,
            IdadeLog = pessoa.IdadeLog,
            UsuarioId = pessoa.UsuarioId,

            Vacinas = await dbContext.Vacina
                .Where(v => pessoa.Vacinas.Select(vp => vp.VacinaId).Contains(v.Id))
                .Select(v => new PessoaVacinaDTO
                {
                    Id = v.Id,
                    Nome = v.Nome,
                    Descricao = v.Descricao,
                    IdadeRecomendada = v.IdadeRecomendada,
                    TipoCalendario = v.TipoCalendario,
                    TipoDose = v.TipoDose,
                    TipoDoseObs = v.TipoDoseObs,
                }).ToListAsync()
        };

        return pessoaDto;
    }


    public async Task<List<PessoaDto>> GetPessoasByUsuarioId(Guid id)
    {
        return await dbContext.Pessoa
            .AsNoTracking()
            .ProjectToType<PessoaDto>()
            .Where(c => c.UsuarioId == id)
            .OrderByDescending(c => c.TipoPessoa == 1)
            .ThenBy(c => c.DtCriacao)
            .ToListAsync();
    }

    public async Task<Guid> CreateAsync(CreationPessoaDto data)
    {
        var newPessoa = new Pessoa
        {
            Id = Guid.NewGuid(),
            DtCriacao = DateTime.Now,
            Nome = data.Nome,
            UsuarioId = data.UsuarioId,
            Cpf = data.Cpf,
            DtNascimento = data.DtNascimento,
            TipoPessoa = data.TipoPessoa
        };

        await dbContext.Pessoa.AddAsync(newPessoa);
        await dbContext.SaveChangesAsync();

        return newPessoa.Id;
    }

    public async Task UpdateAsync(PessoaDto data)
    => await dbContext.Pessoa
        .AsNoTracking()
        .Where(c => c.Id == data.Id)
        .UpdateAsync(c => new Pessoa
        {
            DtUpdate = DateTime.Now,
            Nome = data.Nome.ToUpper(),
            UsuarioId = data.UsuarioId,
            TipoPessoa = data.TipoPessoa,
            DtNascimento= data.DtNascimento
        });


    public async Task<bool> DeleteAsync(Guid id)
    {
        var pessoa = await dbContext.Pessoa
            .FirstOrDefaultAsync(pu => pu.Id == id);

        if (pessoa == null)
            return false;

        dbContext.Pessoa.Remove(pessoa);
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