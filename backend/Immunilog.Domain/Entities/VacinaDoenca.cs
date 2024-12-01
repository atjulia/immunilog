namespace Immunilog.Domain.Entities;

public class VacinaDoenca
{
    public Guid VacinaId { get; set; }
    public Vacina Vacina { get; set; } = new Vacina();

    public Guid DoencaId { get; set; }
    public Doenca Doenca { get; set; } = new Doenca();
}
