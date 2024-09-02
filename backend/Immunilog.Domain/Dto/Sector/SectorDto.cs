﻿using Immunilog.Domain.Dto.Base;

namespace Immunilog.Domain.Dto.Sector;

public class SectorDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
}
