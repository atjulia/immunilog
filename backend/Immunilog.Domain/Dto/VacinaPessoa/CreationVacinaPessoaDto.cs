using Immunilog.Domain.Dto.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immunilog.Domain.Dto.Vacina;

public class CreationVacinaPessoaDto
{
    public Guid VacinaId { get; set; }
    public Guid PessoaId { get; set; }
    public DateTime DtAplicacao { get; set; }
    public string Reacao { get; set; } = string.Empty;
    public string? ReacaoOutros { get; set; } = string.Empty;
}
