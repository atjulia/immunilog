using Immunilog.Domain.Dto.Base;
using Immunilog.Domain.Dto.Usuario;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immunilog.Domain.Enums;

public enum TipoCalendario
{
    [Description("SUS")]
    Sus = 1,
    [Description("PNI")]
    Pni = 2
}