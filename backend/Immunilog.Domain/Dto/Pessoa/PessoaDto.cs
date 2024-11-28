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
    public int IdadeLog { get; set; }
    public DateTime DtNascimento { get; set; }
    public DateTime DtCriacao { get; set; }
    public DateTime DtUpdate { get; set; }
    public string IdadeFormatada => CalcularIdade(DtNascimento);
    
    [ForeignKey(nameof(UsuarioId))]
    public Guid UsuarioId { get; set; }
    public List<PessoaVacinaDTO> Vacinas { get; set; } = new List<PessoaVacinaDTO>();

    private string CalcularIdade(DateTime dataNascimento)
    {
        int anos = CalcularAnos(dataNascimento);
        int meses = CalcularMeses(dataNascimento, anos);

        return FormatarIdade(anos, meses);
    }

    private int CalcularAnos(DateTime dataNascimento)
    {
        var hoje = DateTime.UtcNow;
        int idade = hoje.Year - dataNascimento.Year;

        if (hoje.Month < dataNascimento.Month || (hoje.Month == dataNascimento.Month && hoje.Day < dataNascimento.Day))
        {
            idade--;
        }

        return idade;
    }

    private int CalcularMeses(DateTime dataNascimento)
    {
        var hoje = DateTime.UtcNow;
        int meses = hoje.Month - dataNascimento.Month;

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

        return meses;
    }

    private string FormatarIdade(int anos, int meses)
    {
        if (anos == 0)
        {
            return meses == 1 ? "1 mês" : $"{meses} meses";
        }
        else if (meses == 0)
        {
            return anos == 1 ? "1 ano" : $"{anos} anos";
        }
        else
        {
            string ano = anos == 1 ? "1 ano" : $"{anos} anos";
            string mes = meses == 1 ? "1 mês" : $"{meses} meses";
            return $"{ano} e {mes}";
        }
    }

}

