using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Contract.IRepository;
using Infrastructure.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class ProductVariationValueService : IProductVariationValueService
    {
        private readonly IProductVariationValueRepository _productVariationValueRepository;
        private readonly IProductRepository _productRepository;
        private readonly IVariationValueService _variationValueService;

        public ProductVariationValueService(IProductVariationValueRepository productVariationValueRepository, IProductRepository productRepository, IVariationValueService variationValueService)
        {
            _productVariationValueRepository = productVariationValueRepository;
            _productRepository = productRepository;
            _variationValueService = variationValueService;
        }
        public async Task<int> Delete(string VariationValueId)
        {
            var result = await _productVariationValueRepository.Delete(VariationValueId);
            return result;
        }

        public async Task<int> Save(List<ProductVariationValues> pvariationValue)
        {
            var result = await _productVariationValueRepository.Save(pvariationValue);
            return result;
        }

        public async Task<ProductVariationValuesViewModel> GetProductVariations(string productId)
        {
            
            var productVariation = await _productVariationValueRepository.GetProductVariationValue(productId);
            if (productVariation.Count() != 0)
            {
                var product = await _productRepository.GetProductById(productId);
            
                ProductVariationValuesViewModel productVariationValuesViewModel = new ProductVariationValuesViewModel
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    CategoryID = product.CategoryID,
                    Price = product.Price,
                    Product_Image = product.Product_Image,
                    SKU = product.SKU,
                    InActive = product.InActive,
                };
                productVariationValuesViewModel.Variations = new List<VariationValueViewModel>();
                foreach (var variation in productVariation)
                {
                    var variations = await _variationValueService.GetById(variation.VariationValueId);
                    productVariationValuesViewModel.Variations.Add(variations);
                }

                return productVariationValuesViewModel;
            }
            return null;
        }
    }
}
