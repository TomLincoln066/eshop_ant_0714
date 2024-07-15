using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class CustomerReviewRequestModel
{
    public int Id { get; set; }
    
    public string CustomerId { get; set; }
    
    [Required(ErrorMessage = "Please enter name of the promotion")]
    [StringLength(256, MinimumLength = 2)]
    public string CustomerName { get; set; }
    
    public string OrderId { get; set; }
    
    [Required(ErrorMessage = "Please enter the orderDate")]
    [DataType(DataType.Date)]
    public DateTime OrderDate { get; set; }
    
    public string ProductId { get; set; }
    
    [Required(ErrorMessage = "Please enter name of the promotion")]
    [StringLength(255, MinimumLength = 2)]
    public string ProductName { get; set; }
    
    public double RatingValue { get; set; }
    
    [Required(ErrorMessage = "Please enter name of the promotion")]
    [StringLength(500, MinimumLength = 2)]
    public string Comment { get; set; }
    
    [Required(ErrorMessage = "Please enter the review date")]
    [DataType(DataType.Date)]
    public DateTime ReviewDate { get; set; }
    
    //public ReviewStatus? Status { get; set; }
}