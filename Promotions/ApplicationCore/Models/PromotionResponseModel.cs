using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class PromotionResponseModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }

    public double Discount { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
}