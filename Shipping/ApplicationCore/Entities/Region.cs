using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Region
{
    public int Id { get; set; }
    
    [MaxLength(255)]
    public string Name { get; set; }
    
    public List<ShipperRegion>ShipperRegions { get; set; }
}