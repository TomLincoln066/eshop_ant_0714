using ApplicationCore.Entities;
using ApplicationCore.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Products, ProductViewModel>();
                cfg.CreateMap<Products, ProductViewModelV2>();
                cfg.CreateMap<CategoryVariation, CategoryVariationViewModel>();
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<VariationValue, VariationValueViewModel>();

                // Reverse Mapping
                cfg.CreateMap<CategoryVariationViewModel, CategoryVariation>();
                cfg.CreateMap<ProductViewModel, Products>();
                cfg.CreateMap<ProductViewModelV2, Products>();
                cfg.CreateMap<ProductCategoryViewModel, ProductCategory>();
                cfg.CreateMap<VariationValueViewModel, VariationValue>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
