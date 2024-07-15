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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IShoppingCartItemService shoppingCartItemService) 
        {
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCartItemService = shoppingCartItemService;
        }

        public async Task<int> DeleteShoppingCart(int Id, Guid customerId)
        {
            await _shoppingCartItemService.DeleteShoppingCartItem(Id);
            var result = await _shoppingCartRepository.DeleteShoppingCart(Id, customerId);
            return result;
        }

        public async Task<ShoppingCartViewModel> GetShoppingCartByCustomerId(int Id, Guid customerId)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var result = await _shoppingCartRepository.GetShoppingCart(Id, customerId);
            var shoppingCartViewModel = mapper.Map<ShoppingCartViewModel>(result);
            var shoppingCartItem = await _shoppingCartItemService.GetShoppingCartItemByCustomerId(result.Id);
            shoppingCartViewModel.ShoppingItems.AddRange(shoppingCartItem);
            return shoppingCartViewModel;
        }

        public async Task<ShoppingCartViewModel> GetShoppingCartByCustomerId(Guid customerId)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var result = await _shoppingCartRepository.GetShoppingCart(customerId);
            if (result != null)
            {
                var shoppingViewModel = mapper.Map<ShoppingCartViewModel>(result);
                var shoppingCartItem = await _shoppingCartItemService.GetShoppingCartItemByCustomerId(result.Id);
                if (shoppingViewModel.ShoppingItems == null)
                {
                    shoppingViewModel.ShoppingItems = new List<ShoppingCartItemViewModel>();
                }
                shoppingViewModel.ShoppingItems.AddRange(shoppingCartItem);

                return shoppingViewModel;
            }
            return null;
        }

        public async Task<int> SaveShoppingCart(ShoppingCartViewModel shoppingCart)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var shoppingcart = mapper.Map<ShoppingCart>(shoppingCart);
            List<ShoppingCartItemViewModel> shoppingCartItemViewModels = new List<ShoppingCartItemViewModel>();
            var customer = await GetShoppingCartByCustomerId(shoppingCart.CustomerId);
            if(customer == null)
            {
                await _shoppingCartRepository.SaveShoppingCart(shoppingcart);
            }
            else
            {
                var existingShoppingCartItem = await _shoppingCartItemService.GetShoppingCartItemByCustomerId(customer.Id);
                if(existingShoppingCartItem.Count() == 0 || existingShoppingCartItem.All(x => shoppingCart.ShoppingItems.All(item => item.ProductId != x.ProductId)))
                {
                    foreach (var item in shoppingCart.ShoppingItems)
                    {
                        item.CartId = customer.Id;
                        shoppingCartItemViewModels.Add(item);
                    }
                    await _shoppingCartItemService.SaveShoppingCartItem(shoppingCartItemViewModels);
                }
                else
                {
                    foreach(var item in existingShoppingCartItem)
                    {
                        shoppingcart.ShoppingItems.ForEach(x =>
                        {
                            if(x.ProductId == item.ProductId)
                            {
                                item.Qty = x.Qty;
                            }
                        });

                        await _shoppingCartItemService.UpdateShoppingCartItem(item);
                    }
                }
            }
            return 0;
        }
    }
}
