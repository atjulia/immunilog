using Immunilog.Domain.Dto.Usuario;
using Immunilog.Repositories.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Immunilog.Services.Services.Usuario;

public interface IUsuarioService
{
    Task<List<UsuarioDto>> GetListAsync();
    Task<UsuarioDto?> GetAsync(Guid id);
    Task<Guid> CreateAsync(CreationUsuarioDto model);
    Task<bool> UpdateAsync(UsuarioDto model);
    Task<bool> DeleteAsync(Guid id);

}

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository usuarioRepository;


    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        this.usuarioRepository = usuarioRepository;
    }

    public async Task<List<UsuarioDto>> GetListAsync()
        => await usuarioRepository.GetListAsync();

    public async Task<UsuarioDto?> GetAsync(Guid id)
        => await usuarioRepository.GetAsync(id);

    public async Task<bool> DeleteAsync(Guid id)
    => await usuarioRepository.DeleteAsync(id);

    public async Task<Guid> CreateAsync(CreationUsuarioDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        var projectId = await usuarioRepository.CreateAsync(model);

        return projectId;
    }

    public async Task<bool> UpdateAsync(UsuarioDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        await usuarioRepository.UpdateAsync(model);

        return true;
    }
}
