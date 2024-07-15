using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IRepository
{
    public interface IProductVariationValueRepository
    {
        Task<int> Save(List<ProductVariationValues> productVariationValues);
        Task<int> Delete(string VariationValueId);
        Task<IEnumerable<ProductVariationValues>> GetProductVariationValue(string productId);
    }
}
