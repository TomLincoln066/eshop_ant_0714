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
    public class CategoryVariationRepository : ICategoryVariationRepository
    {
        private readonly IMongoCollection<CategoryVariation> _categoriesCollection;

        public CategoryVariationRepository(IOptions<CatalogDatabaseSettings> catalogStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            catalogStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                catalogStoreDatabaseSettings.Value.DatabaseName);

            _categoriesCollection = mongoDatabase.GetCollection<CategoryVariation>(
                catalogStoreDatabaseSettings.Value.CategoryVariationCollectionName);
        }

        public async Task<int> CreateCategory(CategoryVariation categories)
        {
            await _categoriesCollection.InsertOneAsync(categories);

            return 0;
        }

      
        public async Task<CategoryVariation> GetCategoryById(string id)
        {
            var categories = await _categoriesCollection.FindSync(x => x.Id == id).FirstOrDefaultAsync();
            return categories;
        }

        public async Task<IEnumerable<CategoryVariation>> GetAllCategoryVariation()
        {
            var categories = await _categoriesCollection.FindSync(_=>true).ToListAsync();
            return categories;
        }

        public async Task<int> RemoveCategory(string id)
        {
            var data = await _categoriesCollection.DeleteOneAsync(x => x.Id == id);
            if (data.DeletedCount == 0)
            {
                return -1;
            }

            return 0;
        }

        public async Task<IEnumerable<CategoryVariation>> GetCategoryVarirationByCategoryId(string CategoryId)
        {
            var categories = _categoriesCollection.FindSync(x => x.CategoryId == CategoryId);
            return categories.ToList();
        }
    }
}
