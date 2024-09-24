using Immunilog.Domain.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immunilog.Domain.Dto.Vacina;

public class CreationVacinaDto
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DtCriacao { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public float IdadeRecomendada { get; set; }
    public int TipoCalendario { get; set; }
    public int TipoDose { get; set; }
    public string? TipoDoseObs { get; set; }

    public List<string> Doencas { get; set; }
}
