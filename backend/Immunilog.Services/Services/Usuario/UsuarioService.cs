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
    private readonly IUsuarioRepository _usuarioRepository;


    public UsuarioService(IUsuarioRepository _usuarioRepository)
    {
        _usuarioRepository = _usuarioRepository;
    }

    public async Task<List<UsuarioDto>> GetListAsync()
        => await _usuarioRepository.GetListAsync();

    public async Task<UsuarioDto?> GetUsuarioById(Guid id)
        => await _usuarioRepository.GetUsuarioById(id);

    public async Task<bool> DeleteAsync(Guid id)
    => await _usuarioRepository.DeleteAsync(id);

    public async Task<Guid> CreateAsync(CreationUsuarioDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        var usuarioId = await _usuarioRepository.CreateAsync(model);

        return usuarioId;
    }

    public async Task<bool> UpdateAsync(UsuarioDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        await _usuarioRepository.UpdateAsync(model);

        return true;
    }
}
