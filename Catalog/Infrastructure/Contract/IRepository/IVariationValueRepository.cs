using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IRepository
{
    public interface IVariationValueRepository
    {
        Task<IEnumerable<VariationValue>> Get(string VariationId);
        Task<int> Save(VariationValue variationValue);
        Task<int> Delete(string VariationValueId);
        Task<VariationValue> GetById(string Id);
    }
}
