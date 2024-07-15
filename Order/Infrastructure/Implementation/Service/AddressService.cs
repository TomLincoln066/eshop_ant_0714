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
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<int> DeleteAddress(int Id)
        {
            var result = await _addressRepository.DeleteAddress(Id);
            return result;
        }

        public async Task<AddressViewModel> GetAddress(int Id)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            
            var result = await _addressRepository.GetAddress(Id);
            var addressViewModel = mapper.Map<AddressViewModel>(result);
            return addressViewModel;
        }

        public async Task<int> SaveAddress(AddressViewModel address)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var addressEntity = mapper.Map<Address>(address);
            var result = await _addressRepository.SaveAddress(addressEntity);
            return result;
        }

        public async Task<int> UpdateAddress(AddressViewModel address)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var addressEntity = mapper.Map<Address>(address);
            var result = await _addressRepository.UpdateAddress(addressEntity);
            return result;
        }

        public async Task<IEnumerable<AddressViewModel>> GetAddressByCustomerId(int customerId)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            List<AddressViewModel> addressViewModels = new List<AddressViewModel>();
            var result = await _addressRepository.GetAddressByCustomerId(customerId);
            foreach (var addressEntity in result)
            {
                var addressViewModel = mapper.Map<AddressViewModel>(addressEntity);
                addressViewModels.Add(addressViewModel);
            }
            
            return addressViewModels;
        }
    }
}
