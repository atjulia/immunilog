using Immunilog.Domain.Dto.Base;

namespace Immunilog.Domain.Entities;

public class Usuario : SimpleEntityBase
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime DtCriacao { get; set; }
    public DateTime? DtUpdate { get; set; }
    public DateTime? UltimoAcesso { get; set; }
    public int Role { get; set; }
}

public class AuthUsuario
{
    public string Email { get; set; }
    public string Senha { get; set; }
}