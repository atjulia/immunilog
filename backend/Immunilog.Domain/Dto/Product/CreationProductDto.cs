using Immunilog.Domain.Dto.Base;
using Immunilog.Domain.Dto.Sector;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immunilog.Domain.Dto.Product;

public class CreationProductDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid SectorId { get; set; }
}
