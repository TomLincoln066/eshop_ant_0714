using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class CustomerReviewResponseModel
{
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public double RatingValue { get; set; }
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; }
    public ReviewStatus Status { get; set; }
}