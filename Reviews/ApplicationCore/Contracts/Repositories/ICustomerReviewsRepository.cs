using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface ICustomerReviewsRepository: IBaseRepository<CustomerReview>
{
    Task<IEnumerable<CustomerReview>> GetReviewsByUserAsync(int id);
    Task<IEnumerable<CustomerReview>> GetReviewsByYearAsync(int year);

    Task<IEnumerable<CustomerReview>> GetReviewsByProductAsync(string id);
}