using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Promotion
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }

    public double Discount { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public List<PromotionDetails>PromotionDetails { get; set; }
}