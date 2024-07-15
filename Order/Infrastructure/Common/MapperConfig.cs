using ApplicationCore.Entities;
using ApplicationCore.ViewModel;
using AutoMapper;

namespace Infrastructure.Common
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderEntity, OrderViewModel>();
                cfg.CreateMap<OrderDetailsEntity, OrderDetailViewModel>();
                cfg.CreateMap<CustomerEntity, CustomerViewModel>();
                cfg.CreateMap<Address, AddressViewModel>();
                cfg.CreateMap<ShoppingCart, ShoppingCartViewModel>();
                cfg.CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>();
                cfg.CreateMap<PaymentMethods, PaymentMethodViewModel>();

                //// Reverse Mapping
                cfg.CreateMap<OrderViewModel, OrderEntity>();
                cfg.CreateMap<OrderDetailViewModel, OrderDetailsEntity>();
                cfg.CreateMap<CustomerViewModel, CustomerEntity>();
                cfg.CreateMap<AddressViewModel, Address>();
                cfg.CreateMap<ShoppingCartItemViewModel, ShoppingCartItem>();
                cfg.CreateMap<ShoppingCartViewModel, ShoppingCart>();
                cfg.CreateMap<PaymentMethodViewModel, PaymentMethods>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
