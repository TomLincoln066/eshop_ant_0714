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

    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Products> _productsCollection;
        public ProductRepository(IOptions<CatalogDatabaseSettings> catalogStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            catalogStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                catalogStoreDatabaseSettings.Value.DatabaseName);

            _productsCollection = mongoDatabase.GetCollection<Products>(
                catalogStoreDatabaseSettings.Value.ProductsCollectionName);
        }

        public async Task<Products> CreateProduct(Products products)
        {
             await _productsCollection.InsertOneAsync(products);
             var result = await _productsCollection.Find(x=>x.Id == products.Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            var products = await _productsCollection.Find(_ => true).ToListAsync();
            return products;
        }

        public async Task<Products> GetProductById(string id)
        {
            var product = await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<Products> GetProductByName(string productName)
        {
            var product = await _productsCollection.Find(x => x.ProductName == productName).FirstOrDefaultAsync();
            return product;
        }

        public async Task<IEnumerable<Products>> GetProductByCategoryId(string id)
        {
            var product = await _productsCollection.Find(x => x.CategoryID == id).ToListAsync();
            return product;
        }

        public async Task<int> ProductTotalCount()
        {
            var data = await _productsCollection.Find(_ => true).ToListAsync();
            return data.Count();
        }

        public async Task<int> InActiveProduct(string id)
        {
            var data = await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (data == null)
            {
                return -1;
            }
            data.InActive = true;
            var a = await UpdateProduct(id, data);
            return a;
        }

        public async Task<int> UpdateProduct(string id, Products products)
        {
            var data = await _productsCollection.ReplaceOneAsync(x => x.Id == id, products);
            if (data.ModifiedCount == 0)
            {
                return -1;
            }

            return 0;
        }

        public async Task<int> DeleteProduct(string id)
        {
            var data = _productsCollection.DeleteOne(x => x.Id == id);
            if (data.DeletedCount == 0)
            {
                return -1;
            }

            return 0;
        }
    }
}
