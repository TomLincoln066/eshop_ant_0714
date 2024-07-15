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
    public class ProductVariationValueRepository : IProductVariationValueRepository
    {
        private readonly IMongoCollection<ProductVariationValues> _pVariationCollection;
        public ProductVariationValueRepository(IOptions<CatalogDatabaseSettings> catalogStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            catalogStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                catalogStoreDatabaseSettings.Value.DatabaseName);

            _pVariationCollection = mongoDatabase.GetCollection<ProductVariationValues>(
                catalogStoreDatabaseSettings.Value.ProductVaraiationValuesCollectionName);
        }
        public async Task<int> Delete(string VariationValueId)
        {
            var data = await _pVariationCollection.DeleteOneAsync(x => x.VariationValueId == VariationValueId);
            if (data.DeletedCount == 0)
            {
                return -1;
            }

            return 0;
        }

        public async Task<int> Save(List<ProductVariationValues> productVariationValues)
        {
            await _pVariationCollection.InsertManyAsync(productVariationValues);
            return 0;
        }

        public async Task<IEnumerable<ProductVariationValues>> GetProductVariationValue(string productId)
        {
            var result = await _pVariationCollection.Find(x => x.ProductId == productId).ToListAsync();
            return result;
        }
    }
}
