using Immunilog.Domain.Dto.Base;

namespace Immunilog.Domain.Dto.Vacina;

public class PessoaVacinaDTO : BaseDto
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DtUpdate { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public float IdadeRecomendada { get; set; }
    public string TipoCalendario { get; set; }
    public string TipoDose { get; set; }
    public string? TipoDoseObs { get; set; }
    public List<string> Doencas { get; set; }
}
