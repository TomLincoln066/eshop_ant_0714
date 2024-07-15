using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface IVariationValueService
    {
        Task<IEnumerable<VariationValueViewModel>> Get(string VariationId);
        Task<int> Save(VariationValueViewModel variationValue);
        Task<int> Delete(string VariationValueId);
        Task<VariationValueViewModel> GetById(string Id);
    }
}
