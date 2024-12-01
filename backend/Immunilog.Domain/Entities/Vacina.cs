using Immunilog.Domain.Dto.Base;

namespace Immunilog.Domain.Entities;

public class Vacina : SimpleEntityBase
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DtCriacao { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public float IdadeRecomendada { get; set; }
    public string TipoCalendario { get; set; } = string.Empty;
    public string TipoDose { get; set; } = string.Empty;
    public string? TipoDoseObs { get; set; }
    public DateTime? DtUpdate { get; set; }

    public virtual ICollection<VacinaDoenca> VacinaDoencas { get; set; } = new List<VacinaDoenca>();
}
