using Immunilog.Domain.Dto.Base;
using Immunilog.Domain.Dto.Usuario;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immunilog.Domain.Enums;

public enum DoseVacina
{
    [Description("Dose única")]
    DoseUnica = 0,
    [Description("1ª dose")]
    DoseUm = 1,
    [Description("2ª dose")]
    DoseDois = 2,
    [Description("3ª dose")]
    DoseTres = 3,
    [Description("Reforço")]
    Reforco = 4,
    [Description("1º reforço")]
    ReforcoUm = 5,
    [Description("2º reforço")]
    ReforcoDois = 6,
}