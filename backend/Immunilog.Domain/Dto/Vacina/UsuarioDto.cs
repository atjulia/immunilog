using Immunilog.Domain.Dto.Base;

namespace Immunilog.Domain.Dto.Vacina;

public class VacinaDto : BaseDto
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DtUpdate { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int IdadeRecomendada { get; set; }
    public List<string> Doencas { get; set; }
}
