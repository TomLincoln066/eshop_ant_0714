using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface IProductVariationValueService
    {
        Task<int> Save(List<ProductVariationValues> variationValue);
        Task<int> Delete(string VariationValueId);
        Task<ProductVariationValuesViewModel> GetProductVariations(string productId);
    }
}
