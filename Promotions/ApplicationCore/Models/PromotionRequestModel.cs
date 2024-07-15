using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class PromotionRequestModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please enter name of the promotion")]
    [StringLength(256, MinimumLength = 2)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Please enter promotion description")]
    [StringLength(5000)]
    public string Description { get; set; }
    
    [Required]
    [Range(0.01, 100.00, ErrorMessage = "Discount must be between 0.01 and 100.00")]
    public double Discount { get; set; }
    
    [Required(ErrorMessage = "Please enter the Promotion Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "Please enter the Promotion End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
}