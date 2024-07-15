using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Common;
using Infrastructure.Contract.IRepository;
using Infrastructure.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ICategoryVariationService _categoryVariationService;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, ICategoryVariationService categoryVariationService)
        {
            _productCategoryRepository = productCategoryRepository;
            _categoryVariationService = categoryVariationService;
        }

      

        public async Task<ProductCategoryViewModel> GetProductCategoryById(string id)
        {
            var category = await _productCategoryRepository.GetProductCategoryById(id);
            var mapper = MapperConfig.InitializeAutomapper();
            var viewCategory = mapper.Map<ProductCategoryViewModel>(category);

            return viewCategory;
        }


        public async Task<ProductCategoryViewModel> CreateProductCategory(ProductCategoryViewModel categories)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var categoryEntity = mapper.Map<ProductCategory>(categories);
            var result = await _productCategoryRepository.CreateProductCategory(categoryEntity);
            var viewCategory = mapper.Map<ProductCategoryViewModel>(result);
            return viewCategory;
        }

        public async Task<int> RemoveProductCategory(string id)
        {
            var result = await _productCategoryRepository.RemoveProductCategory(id);
            return result;
        }

        public async Task<IEnumerable<ProductCategoryViewModel>> GetAllCategory()
        {
            var mapper = MapperConfig.InitializeAutomapper();
            List<ProductCategoryViewModel> categoryViewModels = new List<ProductCategoryViewModel>();
            var result = await _productCategoryRepository.GetAll();
            foreach(var productCategory in result)
            {
                var viewCategory = mapper.Map<ProductCategoryViewModel>(productCategory);
                categoryViewModels.Add(viewCategory);
            }
            return categoryViewModels;
        }

        public async Task<IEnumerable<ProductCategoryViewModel>> GetProductCategoryByParentId(string id)
        {
            List<ProductCategoryViewModel> categoryViewModels = new List<ProductCategoryViewModel>();
            var category = await _productCategoryRepository.GetProductCategoryByParentId(id);
            var mapper = MapperConfig.InitializeAutomapper();
            foreach(var productCategory in category)
            {
                var viewCategory = mapper.Map<ProductCategoryViewModel>(productCategory);
                categoryViewModels.Add(viewCategory);
            }
            return categoryViewModels;
        }
    }
}
