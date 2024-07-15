using ApplicationCore.Entities;
using ApplicationCore.Enum;
using Infrastructure.Contract.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repository
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly OrderDbContext _dbContext;
        public ShoppingCartItemRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> DeleteShoppingCartItem(int cartId)
        {
            var entity = await _dbContext.ShoppingCartItem.Where(x => x.CartId == cartId).ToListAsync();
            if (entity == null)
            {
                return 0;
            }

            _dbContext.ShoppingCartItem.RemoveRange(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteShoppingCartItemById(int shoppingCartItemId)
        {
            var entity = await _dbContext.ShoppingCartItem.Where(x => x.Id == shoppingCartItemId).FirstOrDefaultAsync();
            if (entity == null)
            {
                return 0;
            }

            _dbContext.ShoppingCartItem.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<ShoppingCartItem> GetShoppingCartItemById(int shoppingCartItemId)
        {
            var entity = await _dbContext.ShoppingCartItem.Where(x => x.Id == shoppingCartItemId).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItemListById(int cartId)
        {
            var entity = await _dbContext.ShoppingCartItem.Where(x => x.CartId == cartId).ToListAsync();
            return entity;
        }

        public async Task<int> SaveShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            await _dbContext.ShoppingCartItem.AddAsync(shoppingCartItem);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _dbContext.ShoppingCartItem.Update(shoppingCartItem);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
