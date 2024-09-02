using Immunilog.Domain.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immunilog.Domain.Dto.Usuario;

public class CreationUsuarioDto
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int Role { get; set; }
    public DateTime DtUpdate { get; set; }
}
