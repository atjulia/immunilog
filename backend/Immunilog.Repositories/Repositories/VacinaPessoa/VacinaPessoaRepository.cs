using Immunilog.Domain.Dto.Vacina;
using Immunilog.Domain.Entities;
using Immunilog.Repositories.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Immunilog.Repositories.Repositories;

public interface IVacinaPessoaRepository
{
    Task<Guid> CreateSolicitacaoVacina(CreationVacinaPessoaDto data);
    Task<Guid> CreateVacinaPessoa(CreationVacinaPessoaDto model);
    Task UpdateVacinaPessoa(VacinaPessoaDto model);
}
public class VacinaPessoaRepository : IVacinaPessoaRepository
{
    public ApiBaseDbContext dbContext { get; }

    public VacinaPessoaRepository(ApiBaseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Guid> CreateSolicitacaoVacina(CreationVacinaPessoaDto data)
    {
        //var vacina = await dbContext.Vacina.FirstOrDefaultAsync(c => c.Id == data.Id);

        var newVacinaPessoa = new VacinaPessoa
        {
            Id = Guid.NewGuid(),
            PessoaId = data.PessoaId,
            VacinaId = data.VacinaId,
            DtCriacao = new DateTime(),
            Reacao = data.Reacao,
            ReacaoOutros = data.ReacaoOutros

        };

        await dbContext.SaveChangesAsync();

        return newVacinaPessoa.Id;
    }
    public async Task<Guid> CreateVacinaPessoa(CreationVacinaPessoaDto data)
    {
        //var vacina = await dbContext.Vacina.FirstOrDefaultAsync(c => c.Id == data.Id);

        var newVacinaPessoa = new VacinaPessoa
        {
            Id = Guid.NewGuid(),
            PessoaId = data.PessoaId,
            VacinaId = data.VacinaId,
            DtCriacao = new DateTime(),
            Reacao = data.Reacao,
            ReacaoOutros = data.ReacaoOutros
            
        };

        await dbContext.SaveChangesAsync();

        return newVacinaPessoa.Id;
    }
    public async Task UpdateVacinaPessoa(VacinaPessoaDto data)
    => await dbContext.VacinaPessoa
        .AsNoTracking()
        .Where(c => c.Id == data.Id)
        .UpdateAsync(c => new VacinaPessoa
        {
            DtUpdate = DateTime.Now,
            Reacao = c.Reacao,
            ReacaoOutros = c.ReacaoOutros
        });

}
