using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Common;
using Infrastructure.Contract.IRepository;
using Infrastructure.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class VariationValueService : IVariationValueService
    {
        private readonly IVariationValueRepository _variationValueRepository;

        public VariationValueService(IVariationValueRepository variationValueRepository)
        {
            _variationValueRepository = variationValueRepository;
        }
        public async Task<int> Delete(string VariationValueId)
        {
            var result = await _variationValueRepository.Delete(VariationValueId);
            return result;
        }

        public async Task<IEnumerable<VariationValueViewModel>> Get(string VariationId)
        {
            var variationValueEntity = await _variationValueRepository.Get(VariationId);
            var mapper = MapperConfig.InitializeAutomapper();

            List<VariationValueViewModel> a = new List<VariationValueViewModel>();

            foreach(var data in variationValueEntity)
            {
                var result = mapper.Map<VariationValueViewModel>(data);
                a.Add(result);
            }
            
            return a;
        }

        public async Task<int> Save(VariationValueViewModel variationValue)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var model = mapper.Map<VariationValue>(variationValue);
            var result = await _variationValueRepository.Save(model);
            return result;
        }


        public async Task<VariationValueViewModel> GetById(string Id)
        {
            var variationValueEntity = await _variationValueRepository.GetById(Id);
            var mapper = MapperConfig.InitializeAutomapper();

            var result = mapper.Map<VariationValueViewModel>(variationValueEntity);
            return result;
        }
    }
}
