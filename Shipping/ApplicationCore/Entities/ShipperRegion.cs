namespace ApplicationCore.Entities;

public class ShipperRegion
{
    public int Id { get; set; }
    
    //Navigation Property
    public int RegionId { get; set; }
    public Region Region { get; set; }
    
    //Navigation Property
    public int ShipperId { get; set; }
    public Shipper Shipper { get; set; }
}