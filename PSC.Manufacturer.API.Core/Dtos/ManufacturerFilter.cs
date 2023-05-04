using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PSC.Manufacturer.API.Core.Dtos;

public class ManufacturerFilter
{
    [Range(1, 100)]
    [DefaultValue(50)]
    public int PageSize { get; set; }

    [Range(1, int.MaxValue)]
    [DefaultValue(1)]
    public int PageNumber { get; set; }
    public int? ManufacturerKey { get; set; }
    public string? MfgName { get; set; }
}