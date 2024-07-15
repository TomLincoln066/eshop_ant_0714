using ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartViewModel> GetShoppingCartByCustomerId (int Id, Guid customerId);
        Task<int> SaveShoppingCart (ShoppingCartViewModel shoppingCart);
        Task<int> DeleteShoppingCart(int Id, Guid customerId);
        Task<ShoppingCartViewModel> GetShoppingCartByCustomerId(Guid customerId);
    }
}
