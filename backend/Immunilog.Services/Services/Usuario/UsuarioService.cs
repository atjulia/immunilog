using Immunilog.Domain.Dto.Usuario;
using Immunilog.Repositories.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Immunilog.Services.Services.Usuario;

public interface IUsuarioService
{
    Task<List<UsuarioDto>> GetListAsync();
    Task<UsuarioDto?> GetUsuarioById(Guid id);
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

    public async Task<UsuarioDto?> GetUsuarioById(Guid id)
        => await usuarioRepository.GetUsuarioById(id);

    public async Task<bool> DeleteAsync(Guid id)
    => await usuarioRepository.DeleteAsync(id);

    public async Task<Guid> CreateAsync(CreationUsuarioDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        var usuarioId = await usuarioRepository.CreateAsync(model);

        return usuarioId;
    }

    public async Task<bool> UpdateAsync(UsuarioDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        await usuarioRepository.UpdateAsync(model);

        return true;
    }
}
