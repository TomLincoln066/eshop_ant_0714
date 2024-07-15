using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class CustomerReview
{
    public int Id { get; set; }
    
    public string CustomerId { get; set; }
    
    [MaxLength(255)]
    public string CustomerName { get; set; }
    
    public string OrderId { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    public string ProductId { get; set; }
    
    [MaxLength(255)]
    public string ProductName { get; set; }
    
    public double RatingValue { get; set; }
    
    [MaxLength(500)]
    public string Comment { get; set; }
    
    public DateTime ReviewDate { get; set; }
    
    public ReviewStatus Status { get; set; }

}