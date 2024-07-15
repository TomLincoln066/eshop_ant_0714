using ApplicationCore.Entities;
using ApplicationCore.ViewModel;
using Infrastructure.Common;
using Infrastructure.Contract.IRepository;
using Infrastructure.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Service
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        public ShoppingCartItemService(IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public async Task<int> DeleteShoppingCartItem(int cartId)
        {
            var result = await _shoppingCartItemRepository.DeleteShoppingCartItem(cartId);
            return result;
        }

        public async Task<int> DeleteShoppingCartItemById(int ShoppingCarttId)
        {
            var result = await _shoppingCartItemRepository.DeleteShoppingCartItemById(ShoppingCarttId);
            return result;
        }

        public async Task<List<ShoppingCartItemViewModel>> GetShoppingCartItemByCustomerId(int CartId)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var result = await _shoppingCartItemRepository.GetShoppingCartItemListById(CartId);
            List<ShoppingCartItemViewModel> shoppingCartItemViewModels = new List<ShoppingCartItemViewModel>();
            foreach(var item in result)
            {
                var shoppingViewMdoel = mapper.Map<ShoppingCartItemViewModel>(item);
                shoppingCartItemViewModels.Add(shoppingViewMdoel);
            }
            return shoppingCartItemViewModels;
        }

        public async Task<int> SaveShoppingCartItem(List<ShoppingCartItemViewModel> shoppingCart)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            foreach (var item in shoppingCart)
            {
                var shoppingCartItemEntity = mapper.Map<ShoppingCartItem>(item);
                await _shoppingCartItemRepository.SaveShoppingCartItem(shoppingCartItemEntity);
            }

            return 0;
        }

        public async Task<int> UpdateShoppingCartItem(ShoppingCartItemViewModel shoppingCart)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var shoppingCartItemEntity = mapper.Map<ShoppingCartItem>(shoppingCart);
            var result = await _shoppingCartItemRepository.UpdateShoppingCartItem(shoppingCartItemEntity);
            return result;  
        }
    }
}
