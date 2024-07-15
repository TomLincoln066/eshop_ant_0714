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
    public class VariationValueRepository : IVariationValueRepository
    {
        private readonly IMongoCollection<VariationValue> _variationValueCollection;

        public VariationValueRepository(IOptions<CatalogDatabaseSettings> catalogStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            catalogStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                catalogStoreDatabaseSettings.Value.DatabaseName);

            _variationValueCollection = mongoDatabase.GetCollection<VariationValue>(
                catalogStoreDatabaseSettings.Value.VariationValueCollectionName);
        }
        public async Task<int> Delete(string VariationValueId)
        {
            var data = await _variationValueCollection.DeleteOneAsync(x => x.Id == VariationValueId);
            if (data.DeletedCount == 0)
            {
                return -1;
            }

            return 0;
        }

        public async Task<IEnumerable<VariationValue>> Get(string VariationId)
        {
            var data = await _variationValueCollection.Find(x => x.VariationId == VariationId).ToListAsync();
            return data;
        }

        public async Task<VariationValue> GetById(string Id)
        {
            var data = await _variationValueCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<int> Save(VariationValue variationValue)
        {
            await _variationValueCollection.InsertOneAsync(variationValue);
            return 0;
        }
    }
}
