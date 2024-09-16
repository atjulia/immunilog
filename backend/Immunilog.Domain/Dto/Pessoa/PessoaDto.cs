using Immunilog.Domain.Dto.Base;
using Immunilog.Domain.Dto.Usuario;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immunilog.Domain.Dto.Pessoa;

public class PessoaDto : BaseDto
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public int TipoPessoa { get; set; }
    public DateTime DtNascimento { get; set; }
    public DateTime DtCriacao { get; set; }
    public DateTime DtUpdate { get; set; }
    public string IdadeFormatada => CalcularIdade(DtNascimento);
    
    [ForeignKey(nameof(UsuarioId))]
    public Guid UsuarioId { get; set; }

    private string CalcularIdade(DateTime dataNascimento)
    {
        var hoje = DateTime.UtcNow;
        var idade = hoje.Year - dataNascimento.Year;

        if (hoje.Month < dataNascimento.Month || (hoje.Month == dataNascimento.Month && hoje.Day < dataNascimento.Day))
        {
            idade--;
        }

        var meses = hoje.Month - dataNascimento.Month;
        if (meses < 0)
        {
            meses += 12;
        }

        if (meses == 0)
        {
            return $"{idade} anos";
        } else if (idade == 0)
        {
            return $"{meses} meses";
        } else
        {
            return $"{idade} anos e {meses} meses";
        }

    }
}

