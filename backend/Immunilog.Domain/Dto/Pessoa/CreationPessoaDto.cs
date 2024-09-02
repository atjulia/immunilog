using Immunilog.Domain.Dto.Base;
using Immunilog.Domain.Dto.Pessoa;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immunilog.Domain.Dto.Pessoa;

public class CreationPessoaDto
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public int TipoPessoa { get; set; }
    public DateTime DtNascimento { get; set; }
    public DateTime DtCriacao { get; set; }
    public DateTime DtUpdate { get; set; }
    public Guid UsuarioId { get; set; }
}
