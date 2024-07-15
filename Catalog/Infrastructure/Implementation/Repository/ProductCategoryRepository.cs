using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Contract.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly IMongoCollection<ProductCategory> _pCategoryCollection;

        public ProductCategoryRepository(IOptions<CatalogDatabaseSettings> catalogStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            catalogStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                catalogStoreDatabaseSettings.Value.DatabaseName);

            _pCategoryCollection = mongoDatabase.GetCollection<ProductCategory>(
                catalogStoreDatabaseSettings.Value.ProductCategoryCollectionName);
        }

        public async Task<ProductCategory> CreateProductCategory(ProductCategory productCategory)
        {
            await _pCategoryCollection.InsertOneAsync(productCategory);
            var result = await _pCategoryCollection.Find(x=>x.Id == productCategory.Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            var result = await _pCategoryCollection.Find(_=>true).ToListAsync();
            return result;
        }

        public async Task<ProductCategory> GetProductCategoryById(string ProductCategoryId)
        {
            var categories = await _pCategoryCollection.Find(x => x.Id == ProductCategoryId).FirstOrDefaultAsync();
            return categories;
        }

        public async Task<IEnumerable<ProductCategory>> GetProductCategoryByParentId(string Id)
        {
            var categories = await _pCategoryCollection.Find(x => x.Parent_CategoryId == Id).ToListAsync();
            return categories;
        }

        public async Task<int> RemoveProductCategory(string ProductCategoryId)
        {
            var data = await _pCategoryCollection.DeleteOneAsync(x => x.Id == ProductCategoryId);
            if (data.DeletedCount == 0)
            {
                return -1;
            }

            return 0;
        }
    }
}
