using ApplicationCore.Entities;
using ApplicationCore.Models;
using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Contract.IRepository;
using Infrastructure.Contract.IService;
using Infrastructure.Implementation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class CategoryVariationService : ICategoryVariationService
    {
        private readonly ICategoryVariationRepository _categoryVariationRepository;
        private readonly IVariationValueService _variationValueService;
        public CategoryVariationService(ICategoryVariationRepository categoryVariationRepository, IVariationValueService variationValueService) 
        {
            _categoryVariationRepository = categoryVariationRepository;
            _variationValueService = variationValueService;
        }
        public async Task<int> CreateCategory(List<CategoryVariationViewModel> categories)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            foreach (var category in categories)
            {
                var categoryEntity = mapper.Map<CategoryVariation>(category);
                await _categoryVariationRepository.CreateCategory(categoryEntity);

                foreach (var variation in category.Variations)
                {
                    variation.VariationId = categoryEntity.Id;
                    await _variationValueService.Save(variation);
                }
            }
            return 0;
        }

        public async Task<CategoryVariationViewModel> GetCategoryById(string CategoryVariationId)
        {
            var categoryEntity = await _categoryVariationRepository.GetCategoryById(CategoryVariationId);
            if (categoryEntity != null)
            {
                var variation = await _variationValueService.Get(categoryEntity.Id);
                var mapper = MapperConfig.InitializeAutomapper();
                var result = mapper.Map<CategoryVariationViewModel>(categoryEntity);
                result.Variations = variation.ToList();
                return result;
            }
            return null;
        }

        public async Task<int> RemoveCategory(string CategoryVariationId)
        {
            var result = await _categoryVariationRepository.RemoveCategory(CategoryVariationId);    
            return result;
        }

        public async Task<IEnumerable<CategoryVariationViewModel>> GetAllCategoryVariation()
        {
            var category_variation = await _categoryVariationRepository.GetAllCategoryVariation();
            var mapper = MapperConfig.InitializeAutomapper();

            List<CategoryVariationViewModel> a = new List<CategoryVariationViewModel>();

            foreach (var data in category_variation)
            {
                var variation = await _variationValueService.Get(data.Id);

                var result = mapper.Map<CategoryVariationViewModel>(data);

                result.Variations = variation.ToList();

                a.Add(result);
            }

            return a;
        }

        public async Task<IEnumerable<CategoryVariationViewModel>> GetCategoriesByCategoryId(string CategoryId)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var categories = await _categoryVariationRepository.GetCategoryVarirationByCategoryId(CategoryId);
            List<CategoryVariationViewModel> a = new List<CategoryVariationViewModel>();

            foreach (var data in categories)
            {
                var variation = await _variationValueService.Get(data.Id);
                var result = mapper.Map<CategoryVariationViewModel>(data);
                result.Variations = variation.ToList();
                a.Add(result);
            }

            return a;
        }
    }
}
