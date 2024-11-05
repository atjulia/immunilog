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
    Task UpdateVacinaPessoa(VacinaPessoaDto model);
    Task<IEnumerable<VacinaPessoaDto>> GetVacinasByPessoaId(Guid pessoaId);
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
            DtCriacao = new DateTime(),
            Reacao = model.Reacao,
            ReacaoOutros = model.ReacaoOutros
        };

        await dbContext.VacinaPessoa.AddAsync(newVacinaPessoa);
        await dbContext.SaveChangesAsync();

        return newVacinaPessoa.Id;
    }
    public async Task<IEnumerable<VacinaPessoaDto>> GetVacinasByPessoaId(Guid pessoaId)
    {
        var vacinaPessoas = await dbContext.VacinaPessoa
            .AsNoTracking()
            .Where(vp => vp.PessoaId == pessoaId)
            .ToListAsync();

        if (!vacinaPessoas.Any())
            return Enumerable.Empty<VacinaPessoaDto>();

        var vacinaIds = vacinaPessoas.Select(vp => vp.VacinaId).Distinct();

        var vacinas = await dbContext.Vacina
            .AsNoTracking()
            .Where(v => vacinaIds.Contains(v.Id))
            .ProjectToType<VacinaPessoaDto>()
            .ToListAsync();

        return vacinas;
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
