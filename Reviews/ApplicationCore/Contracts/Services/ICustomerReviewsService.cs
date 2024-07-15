using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface ICustomerReviewsService
{
    Task<IEnumerable<CustomerReviewResponseModel>> GetAllReviewsAsync();
    Task<int> AddReviewsAsync(CustomerReviewRequestModel model);
    Task<int> UpdateReviewsAsync(CustomerReviewRequestModel model);
    Task<int> DeleteReviewsAsync(int id);
    Task<CustomerReviewResponseModel> GetReviewsByIdAsync(int id);
    Task<IEnumerable<CustomerReviewResponseModel>> GetReviewsByUser(int id);
    Task<IEnumerable<CustomerReviewResponseModel>> GetReviewsByYear(int year);
    Task<bool> UpdateReviewStatusAsync(int reviewId, ReviewStatus newStatus);
    Task<IEnumerable<CustomerReviewResponseModel>> GetReviewsByProduct(string productId);
}