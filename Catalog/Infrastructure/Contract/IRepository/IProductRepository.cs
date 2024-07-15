using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllProducts();
        Task<Products> GetProductById(string id);
        Task<Products> CreateProduct(Products products);
        Task<int> UpdateProduct(string id, Products products);
        Task<int> InActiveProduct(string id);
        Task<int> ProductTotalCount();
        Task<IEnumerable<Products>> GetProductByCategoryId(string id);
        Task<Products> GetProductByName(string productName);
        Task<int> DeleteProduct(string id);
    }
}
