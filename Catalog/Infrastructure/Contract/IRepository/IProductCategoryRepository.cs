using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IRepository
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetAll();
        Task<ProductCategory> GetProductCategoryById(string ProductCategoryId);
        Task<ProductCategory> CreateProductCategory(ProductCategory productCategory);
        Task<int> RemoveProductCategory(string ProductCategoryId);
        Task<IEnumerable<ProductCategory>> GetProductCategoryByParentId(string Id);
    }
}
