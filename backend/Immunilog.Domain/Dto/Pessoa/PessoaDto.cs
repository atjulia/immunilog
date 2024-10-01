using Immunilog.Domain.Dto.Base;
using Immunilog.Domain.Dto.Usuario;
using Immunilog.Domain.Dto.Vacina;
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
    public List<PessoaVacinaDTO> Vacinas { get; set; } = new List<PessoaVacinaDTO>();

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

        if (hoje.Day < dataNascimento.Day)
        {
            meses--;
        }

        if (meses < 0)
        {
            meses += 12;
        }

        if (idade == 0)
        {
            return meses == 1 ? "1 mês" : $"{meses} meses";
        }
        else if (meses == 0)
        {
            return idade == 1 ? "1 ano" : $"{idade} anos";
        }
        else
        {
            string ano = idade == 1 ? "1 ano" : $"{idade} anos";
            string mes = meses == 1 ? "1 mês" : $"{meses} meses";
            return $"{ano} e {mes}";
        }
    }

}

