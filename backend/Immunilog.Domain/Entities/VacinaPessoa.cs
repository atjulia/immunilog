using Immunilog.Domain.Dto.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immunilog.Domain.Entities;

public class VacinaPessoa : SimpleEntityBase
{
    public Guid VacinaId { get; set; }
    public Guid PessoaId { get; set; }
    public DateTime DtCriacao { get; set; }
    public DateTime DtUpdate { get; set; }
    public DateTime DtAplicacao { get; set; }
    public string Reacao { get; set; } = string.Empty;
    public string ReacaoOutros { get; set; } = string.Empty;
}
