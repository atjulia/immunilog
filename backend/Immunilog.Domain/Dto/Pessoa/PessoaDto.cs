using Immunilog.Domain.Dto.Base;
using Immunilog.Domain.Dto.Usuario;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immunilog.Domain.Dto.Pessoa;

public class PessoaDto : BaseDto
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public int TipoPessoa { get; set; }
    public DateTime DtNascimento { get; set; }
    public DateTime DtCriacao { get; set; }
    public DateTime DtUpdate { get; set; }
    public Guid UsuarioId { get; set; }

    [ForeignKey(nameof(UsuarioId))]
    public UsuarioDto Usuario { get; set; } = default!;

    //public Guid UpdatedById { get; set; }
}
