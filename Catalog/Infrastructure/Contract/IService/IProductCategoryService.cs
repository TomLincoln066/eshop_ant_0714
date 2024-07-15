using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface IProductCategoryService
    {
        Task<ProductCategoryViewModel> GetProductCategoryById(string id);
        Task<ProductCategoryViewModel> CreateProductCategory(ProductCategoryViewModel categories);
        Task<int> RemoveProductCategory(string id);
        Task<IEnumerable<ProductCategoryViewModel>> GetAllCategory();
        Task<IEnumerable<ProductCategoryViewModel>> GetProductCategoryByParentId(string id);
    }
}
