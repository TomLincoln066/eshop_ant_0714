using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using AutoMapper;

namespace Infrastructure.Services;

public class CustomerReviewsService: ICustomerReviewsService
{
    private readonly ICustomerReviewsRepository _customerReviewsRepository;
    private readonly IMapper _mapper;

    public CustomerReviewsService(ICustomerReviewsRepository customerReviewsRepository, IMapper mapper)
    {
        _customerReviewsRepository = customerReviewsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerReviewResponseModel>> GetAllReviewsAsync()
    {
        var reviews = await _customerReviewsRepository.GetAllAsync();
        var responseModels = _mapper.Map<IEnumerable<CustomerReviewResponseModel>>(reviews);
        return responseModels;

    }

    public Task<int>AddReviewsAsync(CustomerReviewRequestModel model)
    {
        var reviewEntity = _mapper.Map<CustomerReview>(model);
       return _customerReviewsRepository.AddAsync(reviewEntity);
  
    }

    public async Task<int> UpdateReviewsAsync(CustomerReviewRequestModel model)
    {
        var existingReview = await _customerReviewsRepository.GetByIdAsync(model.Id);
        _mapper.Map(model, existingReview);
        return await _customerReviewsRepository.UpdateAsync(existingReview);
    }

    public Task<int> DeleteReviewsAsync(int id)
    {
        return _customerReviewsRepository.DeleteAsync(id);
    }

    public async Task<CustomerReviewResponseModel> GetReviewsByIdAsync(int id)
    {
        var review = await _customerReviewsRepository.GetByIdAsync(id); 
        if (review == null)
        {
            return null;
        }

        var promotionResponseModel = _mapper.Map<CustomerReviewResponseModel>(review);
        return promotionResponseModel;
    }

    public async Task<IEnumerable<CustomerReviewResponseModel>> GetReviewsByUser(int id)
    {
        var reviews = await _customerReviewsRepository.GetReviewsByUserAsync(id);
        if (reviews == null)
        {
            return null;
        }
        
        var responseModels = _mapper.Map<IEnumerable<CustomerReviewResponseModel>>(reviews);
        return responseModels;
    }
    
    public async Task<IEnumerable<CustomerReviewResponseModel>>GetReviewsByProduct(string id)
    {
        var reviews = await _customerReviewsRepository.GetReviewsByProductAsync(id);
        if (reviews == null)
        {
            return null;
        }
        
        var responseModels = _mapper.Map<IEnumerable<CustomerReviewResponseModel>>(reviews);
        return responseModels;
    }

    public async Task<IEnumerable<CustomerReviewResponseModel>> GetReviewsByYear(int year)
    {
        var reviews = await _customerReviewsRepository.GetReviewsByYearAsync(year);
        var responseModels = _mapper.Map<IEnumerable<CustomerReviewResponseModel>>(reviews);
        return responseModels;
    }
    
    public async Task<bool> UpdateReviewStatusAsync(int reviewId, ReviewStatus newStatus)
    {
        var review = await _customerReviewsRepository.GetByIdAsync(reviewId);
        if (review == null)
        {
            return false; // Review not found
        }

        review.Status = newStatus;
        return await _customerReviewsRepository.UpdateAsync(review) > 0;
    }

   
    }

