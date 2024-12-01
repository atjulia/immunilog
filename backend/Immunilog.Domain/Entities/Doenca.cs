using Immunilog.Domain.Dto.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immunilog.Domain.Entities;

public class Doenca : SimpleEntityBase
{
    public string Nome { get; set; } = string.Empty;
    public virtual ICollection<VacinaDoenca> VacinaDoencas { get; set; } = new List<VacinaDoenca>();
}