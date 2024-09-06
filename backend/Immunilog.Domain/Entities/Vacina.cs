using Immunilog.Domain.Dto.Base;

namespace Immunilog.Domain.Entities;

public class Vacina : SimpleEntityBase
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DtCriacao { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int IdadeRecomendada { get; set; }
    public DateTime? DtUpdate { get; set; }

    public virtual ICollection<VacinaDoenca> VacinaDoencas { get; set; } = new List<VacinaDoenca>();
}
