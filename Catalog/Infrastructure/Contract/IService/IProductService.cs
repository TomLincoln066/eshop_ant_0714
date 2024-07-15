using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface IProductService
    {
        Task<GetAllProductswithTotalCount> GetAllProducts(int pageNumber, int pageSize, string CategoryName);
        Task<ProductViewModel> GetProductById(string id);
        Task<ProductViewModelV2> CreateProduct(ProductViewModelV2 products);
        Task<int> UpdateProduct(string id, ProductViewModelV2 products);
        Task<int> InActiveProduct(string id);
        Task<IEnumerable<ProductViewModelV2>> GetProductByCategoryId(string id);
        Task<ProductViewModel> GetProductByName(string productName);
        Task<int> DeleteProduct(string id);
    }
}
