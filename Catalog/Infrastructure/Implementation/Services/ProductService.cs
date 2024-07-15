using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Common;
using Infrastructure.Contract.IRepository;
using Infrastructure.Contract.IService;
using Infrastructure.Implementation.Repository;

namespace Infrastructure.Implementation.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryService _pcategoryService;
        private readonly ICategoryVariationService _pcategoryVariationService;
        private readonly IVariationValueService _variationValueService;

        public ProductService(IProductRepository productRepository, IProductCategoryService pcategoryService, ICategoryVariationService categoryVariationService, IVariationValueService variationValueService)
        {
            _productRepository = productRepository;
            _pcategoryService = pcategoryService;
            _pcategoryVariationService = categoryVariationService;
            _variationValueService = variationValueService;
        }
        

        public async Task<ProductViewModelV2> CreateProduct(ProductViewModelV2 products)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var productEntity = mapper.Map<Products>(products);
            var result =  await _productRepository.CreateProduct(productEntity);
            products = mapper.Map<ProductViewModelV2>(result);
            return products;
        }

        public async Task<GetAllProductswithTotalCount> GetAllProducts(int pageNumber, int pageSize, string CategoryName)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var products = await _productRepository.GetAllProducts();
            if (products.Count() != 0)
            {
                List<ProductViewModel> productsViewModels = new List<ProductViewModel>();
                foreach (var product in products)
                {
                    var productViewModel = mapper.Map<ProductViewModel>(product);
                    // Category and SubCategory
                    var ssubproductCategory = await _pcategoryService.GetProductCategoryById(product.CategoryID);
                    var productCategory = await _pcategoryService.GetProductCategoryById(ssubproductCategory.Parent_CategoryId);
                    
                    if (productCategory.Name == CategoryName)
                    {
                        if (productViewModel.ProductCategory == null)
                        {
                            productViewModel.ProductCategory = new List<ProductCategoryViewModel>();
                        }
                        productViewModel.ProductCategory.Add(productCategory);
                        productViewModel.ProductCategory.Add(ssubproductCategory);

                        // Category Variation

                        var categoryVariation = await _pcategoryVariationService.GetCategoriesByCategoryId(product.CategoryID);

                        if (productViewModel.CategoryVariation == null)
                        {
                            productViewModel.CategoryVariation = new List<CategoryVariationViewModel>();
                        }

                        productViewModel.CategoryVariation = categoryVariation.ToList();


                        productsViewModels.Add(productViewModel);
                    }
                }

                GetAllProductswithTotalCount getAllProductswithTotalCount = new()
                {
                    TotalProduct = productsViewModels.Count(),
                    products = productsViewModels.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                };
                return getAllProductswithTotalCount;
            }
            return null;
        }

        public async Task<ProductViewModel> GetProductById(string id)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var product = await _productRepository.GetProductById(id);
            if (product != null)
            {
                var productViewModel = mapper.Map<ProductViewModel>(product);
                var ssubproductCategory = await _pcategoryService.GetProductCategoryById(product.CategoryID);
                var subproductCategory = await _pcategoryService.GetProductCategoryById(ssubproductCategory.Parent_CategoryId);

                
                if (productViewModel.ProductCategory == null)
                {
                    productViewModel.ProductCategory = new List<ProductCategoryViewModel>();
                }

                productViewModel.ProductCategory.Add(subproductCategory);
                productViewModel.ProductCategory.Add(ssubproductCategory);
                // Category Variation

                var categoryVariation = await _pcategoryVariationService.GetCategoriesByCategoryId(product.CategoryID);

                if (productViewModel.CategoryVariation == null)
                {
                    productViewModel.CategoryVariation = new List<CategoryVariationViewModel>();
                }

                productViewModel.CategoryVariation = categoryVariation.ToList();


                return productViewModel;
            }
            return null;
        }

        public async Task<int> InActiveProduct(string id)
        {
            var product = await _productRepository.InActiveProduct(id);
            return product;
        }

        public async Task<int> UpdateProduct(string id, ProductViewModelV2 products)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var productEntity = mapper.Map<Products>(products);
            var result = await _productRepository.UpdateProduct(id, productEntity);
           
            return result;
        }

        public async Task<IEnumerable<ProductViewModelV2>> GetProductByCategoryId(string id)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            List<ProductViewModelV2> productViews = new List<ProductViewModelV2>();
            var result = await _productRepository.GetProductByCategoryId(id);
            foreach (var item in result)
            {
                var productEntity = mapper.Map<ProductViewModelV2>(item);
                productViews.Add(productEntity);
            }
            return productViews;
        }

        public async Task<ProductViewModel> GetProductByName(string productName)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var product = await _productRepository.GetProductByName(productName);
            if (product != null)
            {
                var productViewModel = mapper.Map<ProductViewModel>(product);
                var ssubproductCategory = await _pcategoryService.GetProductCategoryById(product.CategoryID);
                var subproductCategory = await _pcategoryService.GetProductCategoryById(ssubproductCategory.Parent_CategoryId);


                if (productViewModel.ProductCategory == null)
                {
                    productViewModel.ProductCategory = new List<ProductCategoryViewModel>();
                }

                productViewModel.ProductCategory.Add(subproductCategory);
                productViewModel.ProductCategory.Add(ssubproductCategory);
                // Category Variation

                var categoryVariation = await _pcategoryVariationService.GetCategoriesByCategoryId(product.CategoryID);

                if (productViewModel.CategoryVariation == null)
                {
                    productViewModel.CategoryVariation = new List<CategoryVariationViewModel>();
                }

                productViewModel.CategoryVariation = categoryVariation.ToList();

                // Variation Value
                foreach (var category in categoryVariation)
                {
                    var variation = await _variationValueService.Get(category.Id);

                    if (category.Variations == null)
                    {
                        category.Variations = new List<VariationValueViewModel>();
                    }
                    category.Variations.AddRange(variation);
                }

                return productViewModel;
            }
            return null;
        }

        public async Task<int> DeleteProduct(string id)
        {
            var result = await _productRepository.DeleteProduct(id);
            return result;
        }
    }
}
