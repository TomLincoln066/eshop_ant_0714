using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IRepository
{
    public interface IShoppingCartItemRepository
    {
        Task<int> SaveShoppingCartItem(ShoppingCartItem shoppingCartItem);
        Task<int> DeleteShoppingCartItem(int cartId);
        Task<int> UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        Task<ShoppingCartItem> GetShoppingCartItemById(int shoppingCartItemId);
        Task<List<ShoppingCartItem>> GetShoppingCartItemListById(int cartId);
        Task<int> DeleteShoppingCartItemById(int shoppingCartItemId);
    }
}
