using Immunilog.Domain.Dto.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immunilog.Domain.Entities;

public class Pessoa : SimpleEntityBase
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public int TipoPessoa { get; set; }
    public DateTime DtNascimento { get; set; }
    public DateTime DtCriacao { get; set; }
    public DateTime DtUpdate { get; set; }
    public Guid UsuarioId { get; set; }

    [ForeignKey(nameof(UsuarioId))]
    public Usuario Usuario { get; set; } = default!;
    public virtual ICollection<VacinaPessoa> Vacinas { get; set; }

    //public Guid UpdatedById { get; set; }
}
