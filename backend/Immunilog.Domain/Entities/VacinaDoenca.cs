namespace Immunilog.Domain.Entities;

public class VacinaDoenca
{
    public Guid VacinaId { get; set; }
    public Vacina Vacina { get; set; }

    public Guid DoencaId { get; set; }
    public Doenca Doenca { get; set; }
}
