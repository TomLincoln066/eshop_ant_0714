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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressService _addressService;
        public CustomerService(ICustomerRepository customerRepository,  IAddressService addressService) 
        {
            _customerRepository = customerRepository;
            _addressService = addressService;
        }
        public async Task<int> DeleteCustomer(Guid customerId)
        {
            var result = await _customerRepository.DeleteCustomer(customerId);
            return result;
        }

        public async Task<CustomerViewModel> GetCustomerById(Guid customerId)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var customerEntity = await _customerRepository.GetCustomerById(customerId);
            var customerViewModel = mapper.Map<CustomerViewModel>(customerEntity);
            var address = await _addressService.GetAddressByCustomerId(customerEntity.Id);
            customerViewModel.Address = address.ToList();
            return customerViewModel;
        }

        public async Task<int> SaveCustomer(CustomerViewModel customerViewModel)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var customerEntity = mapper.Map<CustomerEntity>(customerViewModel);
            var customer = await _customerRepository.GetCustomerById(customerViewModel.UserId);
            if(customer == null)
            {
                await _customerRepository.SaveCustomer(customerEntity);
            }
            else
            {
                await _customerRepository.UpdateCustomer(customerEntity);
            }

            var addres = await _addressService.GetAddressByCustomerId(customer.Id);
            if (addres.Count() == 0)
            {
                foreach (var address in customerViewModel.Address)
                {
                    address.CustomerId = customer.Id;
                    await _addressService.SaveAddress(address);
                }
            }
            else
            {
                foreach (var address in customerViewModel.Address)
                {
                    address.CustomerId = customer.Id;
                    await _addressService.UpdateAddress(address);
                }
            }
            
            return 0;
        }

        public async Task<int> UpdateCustomer(CustomerViewModel customerViewModel)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var customerEntity = mapper.Map<CustomerEntity>(customerViewModel);
            var result = await _customerRepository.UpdateCustomer(customerEntity);
            return result;
        }
    }
}
