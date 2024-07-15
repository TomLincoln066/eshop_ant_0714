using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Shipper
{
    public int Id { get; set; }
    
    [MaxLength(255)]
    public string Name { get; set; }
    
    [MaxLength(255)]
    public string Email { get; set; }
    
    [RegularExpression(@"^\d{10}$")]  //For 10 digit us phone number
    public string Phone { get; set; }
    
    [MaxLength(255)]
    public string ContactPerson { get; set; }
    
    public List<ShipperRegion> ShipperRegions { get; set; }

}