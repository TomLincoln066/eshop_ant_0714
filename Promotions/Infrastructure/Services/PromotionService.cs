using System.Linq.Expressions;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class PromotionService : IPromotionService
{
    private readonly IPromotionRepository _promotionRepository;
    private IPromotionService _promotionServiceImplementation;

    public PromotionService(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task<int> AddPromotionAsync(PromotionRequestModel model)
    {
        var promotionEntity = new Promotion
        {
            Name = model.Name,
            Description = model.Description,
            Discount = model.Discount,
            StartDate = model.StartDate,
            EndDate = model.EndDate
        };

       var promotion = await _promotionRepository.AddAsync(promotionEntity);
       return promotion.Id;
    }

    public async Task<int> UpdatePromotionAsync(PromotionRequestModel model)
    {
        var existingPromotion = await _promotionRepository.GetByIdAsync(model.Id);
        if (existingPromotion == null)
        {
            throw new Exception("Promotion does not exist");
           
        }
        existingPromotion.Name = model.Name;
        existingPromotion.Description = model.Description;
        existingPromotion.Discount = model.Discount;
        existingPromotion.StartDate = model.StartDate;
        existingPromotion.EndDate = model.EndDate;
        
        return await _promotionRepository.UpdateAsync(existingPromotion);
    }

    public Task<int> DeletePromotionAsync(int id)
    {
        return _promotionRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PromotionResponseModel>> GetAllPromotions()
    {
        var promotions = await _promotionRepository.GetAllAsync();

        var promotionResponseModel = new List<PromotionResponseModel>();

        foreach (var promotion in promotions)
            promotionResponseModel.Add(new PromotionResponseModel
            {
                Id = promotion.Id, 
                Name = promotion.Name,
                Description = promotion.Description,
                Discount = promotion.Discount,
                StartDate = promotion.StartDate, 
                EndDate = promotion.EndDate
            });

        return promotionResponseModel;
    }

    public async Task<PromotionResponseModel> GetPromotionByIdAsync(int id)
    {
        var promotion = await _promotionRepository.GetByIdAsync(id);
        if (promotion == null)
        {
            return null;
        }
        var promotionResponseModel = new PromotionResponseModel
        {
            Id = promotion.Id, 
            Name = promotion.Name,
            Description = promotion.Description,
            Discount = promotion.Discount,
            StartDate = promotion.StartDate, 
            EndDate = promotion.EndDate
        };
        return promotionResponseModel;
    }

    public async Task<IEnumerable<PromotionResponseModel>> GetPromotionByProduct(string productName)
    {
        Expression<Func<Promotion, bool>> filter = p =>
            p.PromotionDetails.Any(pd => pd.ProductName == productName);

        var promotions = await _promotionRepository.GetByConditionAsync(filter);
        
        if (promotions == null || !promotions.Any())
        {
            return null;
        }
        
        return promotions.Select(promotion => new PromotionResponseModel()
        {
            Id = promotion.Id,
            Name = promotion.Name,
            Description = promotion.Description,
            Discount =  promotion.Discount,
            StartDate = promotion.StartDate,
            EndDate = promotion.EndDate
        });
    }

    public async Task<IEnumerable<PromotionResponseModel>> GetAllActivePromotion()
    {
        DateTime currentDate = DateTime.Now;
        Expression<Func<Promotion, bool>> filter = p =>
            p.StartDate <= currentDate && p.EndDate >= currentDate;

        var promotions = await _promotionRepository.GetByConditionAsync(filter);

        if (promotions == null || !promotions.Any())
        {
            return null;
        }

        return promotions.Select(promotion => new PromotionResponseModel()
        {
            Id = promotion.Id,
            Name = promotion.Name,
            Description = promotion.Description,
            Discount = promotion.Discount,
            StartDate = promotion.StartDate,
            EndDate = promotion.EndDate
        });
    }

    //check for promotions starting today and promotions ending today
        
        public async Task<IEnumerable<PromotionResponseModel>> GetPromotionsStartingToday()
        {
            DateTime currentDate = DateTime.Now;
            Expression<Func<Promotion, bool>> filter = p =>
                p.StartDate.Date == currentDate.Date; // Check if promotion starts today

            var promotions = await _promotionRepository.GetByConditionAsync(filter);

            if (promotions == null || !promotions.Any())
            {
                return null;
            }

            return promotions.Select(promotion => new PromotionResponseModel()
            {
                Id = promotion.Id,
                Name = promotion.Name,
                Description = promotion.Description,
                Discount = promotion.Discount,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate
            });
        }

        public async Task<IEnumerable<PromotionResponseModel>> GetPromotionsEndingToday()
        {
            DateTime currentDate = DateTime.Now;
            Expression<Func<Promotion, bool>> filter = p =>
                p.EndDate.Date == currentDate.Date; // Check if promotion ends today

            var promotions = await _promotionRepository.GetByConditionAsync(filter);

            if (promotions == null || !promotions.Any())
            {
                return null;
            }

            return promotions.Select(promotion => new PromotionResponseModel()
            {
                Id = promotion.Id,
                Name = promotion.Name,
                Description = promotion.Description,
                Discount = promotion.Discount,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate
            });
        }

    }
    
