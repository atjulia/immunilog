using Immunilog.Domain.Dto.Pessoa;
using Immunilog.Domain.Entities;
using Immunilog.Repositories.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Immunilog.Repositories.Repositories;

public interface IPessoaRepository
{
    Task<List<PessoaDto>> GetListAsync();
    Task<PessoaDto?> GetAsync(Guid id);
    Task<Guid> CreateAsync(CreationPessoaDto data);
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
    public async Task<List<PessoaDto>> GetListAsync()
    {
        return await dbContext.Pessoa
            .AsNoTracking()
            .ProjectToType<PessoaDto>()
            .ToListAsync();
    }

    public async Task<PessoaDto?> GetAsync(Guid id)
    {
        return await dbContext.Pessoa
            .AsNoTracking()
            .ProjectToType<PessoaDto>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> CreateAsync(CreationPessoaDto data)
    {
        var newPessoa = new Pessoa
        {
            Id = Guid.NewGuid(),
            DtCriacao = DateTime.Now,
            Nome = data.Nome.ToUpper(),
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
}