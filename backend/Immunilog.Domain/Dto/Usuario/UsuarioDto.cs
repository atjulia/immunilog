using Immunilog.Domain.Dto.Base;

namespace Immunilog.Domain.Dto.Usuario;

public class UsuarioDto : BaseDto
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime? DtUpdate { get; set; }
    public int Role { get; set; }
}

public class UsuarioCredentials : BaseDto
{
    public string Email { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public Guid UsuarioId { get; set; }
}
