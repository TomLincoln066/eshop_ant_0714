using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IPromotionService
{
    Task<int> AddPromotionAsync(PromotionRequestModel model);
    Task<int> UpdatePromotionAsync(PromotionRequestModel model);
    Task<int> DeletePromotionAsync(int id);
    Task<IEnumerable<PromotionResponseModel>> GetAllPromotions();
    Task<PromotionResponseModel> GetPromotionByIdAsync(int id);
    Task<IEnumerable<PromotionResponseModel>> GetPromotionByProduct(string productCategoryName);
    Task<IEnumerable<PromotionResponseModel>> GetAllActivePromotion();
    Task<IEnumerable<PromotionResponseModel>> GetPromotionsStartingToday();
    Task<IEnumerable<PromotionResponseModel>> GetPromotionsEndingToday();

}
